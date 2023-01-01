using System.Text.Json;
using BSc_Thesis.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BSc_Thesis.DataBase.Stores;

public class ProcessStore
{
    private readonly ILogger<ProcessStore> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ProcessStore(ILogger<ProcessStore> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }


    public async Task<ProcessDb> GetLastRow()
    {
        try
        {
            _logger.LogInformation("GetLastRow");

            await using var context = _serviceScopeFactory.CreateScope().ServiceProvider
                .GetRequiredService<S7plcSqlContext>();
            return await context.Proces.LastOrDefaultAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "GetLastRow");
            return null;
        }
    }

    public async Task<ProcessDb[]> GetAllRows()
    {
        try
        {
            _logger.LogInformation("GetAllRows");

            await using var context = _serviceScopeFactory.CreateScope().ServiceProvider
                .GetRequiredService<S7plcSqlContext>();

            return context.Proces.ToArray();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "GetAllRows");
            return Array.Empty<ProcessDb>();
        }
    }

    public async Task<ProcessDb[]> GetAllRowsByID(int index)
    {
        try
        {
            _logger.LogInformation("GetAllRowsByID | {Index}", index);

            await using var context = _serviceScopeFactory.CreateScope().ServiceProvider
                .GetRequiredService<S7plcSqlContext>();

            return context.Proces.Where(x => x.ID >= index * 100000 && x.ID <= (index + 1) * 100000).ToArray();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "GetAllRowsByID");
            return Array.Empty<ProcessDb>();
        }
    }

    public async Task<ProcessDb[]> GetFilteredProcess(ProcessFilterRequest filterRequest)
    {
        try
        {
            _logger.LogInformation("GetFilteredProcess | {filter}", JsonSerializer.Serialize(filterRequest));

            await using var context = _serviceScopeFactory.CreateScope().ServiceProvider
                .GetRequiredService<S7plcSqlContext>();

            return filterRequest.FilterBy switch
            {
                ProcessFilterOptions.ID => context.Proces.Where(x =>
                        x.ID >= Convert.ToInt32(filterRequest.From) &&
                        x.ID <= Convert.ToInt32(filterRequest.From))
                    .ToArray(),
                ProcessFilterOptions.Date => context.Proces.Where(x =>
                        x.Date >= Convert.ToDateTime(filterRequest.From) &&
                        x.Date <= Convert.ToDateTime(filterRequest.To))
                    .ToArray(),
                ProcessFilterOptions.Temperature => context.Proces.Where(x =>
                        x.Temperature >= Convert.ToInt32(filterRequest.From) &&
                        x.Temperature <= Convert.ToInt32(filterRequest.From))
                    .ToArray(),
                ProcessFilterOptions.AmbientTemperature => context.Proces.Where(x =>
                        x.AmbientTemperature >= Convert.ToInt32(filterRequest.From) &&
                        x.AmbientTemperature <= Convert.ToInt32(filterRequest.From))
                    .ToArray(),
                ProcessFilterOptions.Glucose => context.Proces.Where(x =>
                        x.Glucose >= Convert.ToInt32(filterRequest.From) &&
                        x.Glucose <= Convert.ToInt32(filterRequest.From))
                    .ToArray(),
                ProcessFilterOptions.Maltose => context.Proces.Where(x =>
                        x.Maltose >= Convert.ToInt32(filterRequest.From) &&
                        x.Maltose <= Convert.ToInt32(filterRequest.From))
                    .ToArray(),
                ProcessFilterOptions.Maltotriosis => context.Proces.Where(x =>
                        x.Maltotriosis >= Convert.ToInt32(filterRequest.From) &&
                        x.Maltotriosis <= Convert.ToInt32(filterRequest.From))
                    .ToArray(),
                ProcessFilterOptions.Sugar => context.Proces.Where(x =>
                        x.Sugar >= Convert.ToInt32(filterRequest.From) &&
                        x.Sugar <= Convert.ToInt32(filterRequest.From))
                    .ToArray(),
                ProcessFilterOptions.DeadYeast => context.Proces.Where(x =>
                        x.DeadYeast >= Convert.ToInt32(filterRequest.From) &&
                        x.DeadYeast <= Convert.ToInt32(filterRequest.From))
                    .ToArray(),
                ProcessFilterOptions.ActiveYeast => context.Proces.Where(x =>
                        x.ActiveYeast >= Convert.ToInt32(filterRequest.From) &&
                        x.ActiveYeast <= Convert.ToInt32(filterRequest.From))
                    .ToArray(),
                ProcessFilterOptions.LatticeYeast => context.Proces.Where(x =>
                        x.LatticeYeast >= Convert.ToInt32(filterRequest.From) &&
                        x.LatticeYeast <= Convert.ToInt32(filterRequest.From))
                    .ToArray(),
                ProcessFilterOptions.Ethanol => context.Proces.Where(x =>
                        x.Ethanol >= Convert.ToInt32(filterRequest.From) &&
                        x.Ethanol <= Convert.ToInt32(filterRequest.From))
                    .ToArray(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "GetAllRowsByID");
            return Array.Empty<ProcessDb>();
        }
    }
}