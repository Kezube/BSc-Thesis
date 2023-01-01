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
        var x = GetAllRows().Result;
    }


    public async Task<ProcessDb> GetLastRow()
    {
        try
        {
            _logger.LogInformation("GetLastRow");

            await using var context = _serviceScopeFactory.CreateScope().ServiceProvider
                .GetRequiredService<S7plcSqlContext>();
            return context.Proces.OrderBy(x=> x.ID).LastOrDefault();
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
                        x.ID <= Convert.ToInt32(filterRequest.To))
                    .ToArray(),
                ProcessFilterOptions.Date => context.Proces.Where(x =>
                        x.Date >= Convert.ToDateTime(filterRequest.From) &&
                        x.Date <= Convert.ToDateTime(filterRequest.To))
                    .ToArray(),
                ProcessFilterOptions.Temperature => context.Proces.Where(x =>
                        x.Temperature >= Convert.ToDouble(filterRequest.From) &&
                        x.Temperature <= Convert.ToDouble(filterRequest.To))
                    .ToArray(),
                ProcessFilterOptions.AmbientTemperature => context.Proces.Where(x =>
                        x.AmbientTemperature >= Convert.ToDouble(filterRequest.From) &&
                        x.AmbientTemperature <= Convert.ToDouble(filterRequest.To))
                    .ToArray(),
                ProcessFilterOptions.Glucose => context.Proces.Where(x =>
                        x.Glucose >= Convert.ToDouble(filterRequest.From) &&
                        x.Glucose <= Convert.ToDouble(filterRequest.To))
                    .ToArray(),
                ProcessFilterOptions.Maltose => context.Proces.Where(x =>
                        x.Maltose >= Convert.ToDouble(filterRequest.From) &&
                        x.Maltose <= Convert.ToDouble(filterRequest.To))
                    .ToArray(),
                ProcessFilterOptions.Maltotriosis => context.Proces.Where(x =>
                        x.Maltotriosis >= Convert.ToDouble(filterRequest.From) &&
                        x.Maltotriosis <= Convert.ToDouble(filterRequest.To))
                    .ToArray(),
                ProcessFilterOptions.Sugar => context.Proces.Where(x =>
                        x.Sugar >= Convert.ToDouble(filterRequest.From) &&
                        x.Sugar <= Convert.ToDouble(filterRequest.To))
                    .ToArray(),
                ProcessFilterOptions.DeadYeast => context.Proces.Where(x =>
                        x.DeadYeast >= Convert.ToDouble(filterRequest.From) &&
                        x.DeadYeast <= Convert.ToDouble(filterRequest.To))
                    .ToArray(),
                ProcessFilterOptions.ActiveYeast => context.Proces.Where(x =>
                        x.ActiveYeast >= Convert.ToDouble(filterRequest.From) &&
                        x.ActiveYeast <= Convert.ToDouble(filterRequest.To))
                    .ToArray(),
                ProcessFilterOptions.LatticeYeast => context.Proces.Where(x =>
                        x.LatticeYeast >= Convert.ToDouble(filterRequest.From) &&
                        x.LatticeYeast <= Convert.ToDouble(filterRequest.To))
                    .ToArray(),
                ProcessFilterOptions.Ethanol => context.Proces.Where(x =>
                        x.Ethanol >= Convert.ToDouble(filterRequest.From) &&
                        x.Ethanol <= Convert.ToDouble(filterRequest.To))
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