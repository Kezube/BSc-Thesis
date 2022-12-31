using BSc_Thesis.DataBase.Events;
using BSc_Thesis.DataBase.Models;
using BSc_Thesis.DataBase.Stores;
using Easy.MessageHub;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BSc_Thesis.DataBase
{
    public class ProcessMonitor : BackgroundService
    {
        private readonly ILogger<ProcessMonitor> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMessageHub _messageHub;
        private ProcessDb _lastRow;


        public ProcessMonitor(IServiceScopeFactory serviceScopeFactory, ILogger<ProcessMonitor> logger )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _messageHub = new MessageHub();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Ticket expired monitor running.");
            await DoWorkTicketExpiredMonitor(stoppingToken);
        }

        private async Task DoWorkTicketExpiredMonitor(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    _logger.LogInformation("Ticket expired monitor loop iteration...");
                    var ticketRepository = scope.ServiceProvider.GetRequiredService<ProcessStore>();
                    var lastCurrentRow = await ticketRepository.GetLastRow();

                    if (_lastRow == null)
                        _lastRow = lastCurrentRow;

                    if (_lastRow.ID != lastCurrentRow.ID)
                    {
                        _lastRow = lastCurrentRow;
                        
                    }

                    _messageHub.Publish(new ProcessUpdated(lastCurrentRow));
                }
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Ticket expired monitor is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}