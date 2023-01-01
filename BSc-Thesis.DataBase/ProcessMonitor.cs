using BSc_Thesis.DataBase.Events;
using BSc_Thesis.DataBase.Models;
using BSc_Thesis.DataBase.Stores;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BSc_Thesis.DataBase;

public class ProcessMonitor : BackgroundService
{
    private readonly ILogger<ProcessMonitor> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private ProcessDb _lastRow;


    public ProcessMonitor(IServiceScopeFactory serviceScopeFactory, ILogger<ProcessMonitor> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Process monitor running");
        await DoWorkTicketExpiredMonitor(stoppingToken);
    }

    private async Task DoWorkTicketExpiredMonitor(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            _logger.LogInformation("Process monitor loop iteration...");

            using var scope = _serviceScopeFactory.CreateScope();
            var ticketRepository = scope.ServiceProvider.GetRequiredService<IProcessStore>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var lastCurrentRow = await ticketRepository.GetLast();

            _lastRow ??= lastCurrentRow;

            if (_lastRow is null || lastCurrentRow is null)
                continue;

            if (_lastRow.ID == lastCurrentRow.ID)
                continue;

            _lastRow = lastCurrentRow;
            await mediator.Publish(new ProcessUpdated(), stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Process monitor is stopping");
        await base.StopAsync(stoppingToken);
    }
}