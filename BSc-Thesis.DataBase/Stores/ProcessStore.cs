using System.Linq.Expressions;
using System.Text.Json;
using BSc_Thesis.DataBase.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BSc_Thesis.DataBase.Stores;

public class ProcessStore : IProcessStore
{
    private readonly ILogger<ProcessStore> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ProcessStore(ILogger<ProcessStore> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    public async Task<ProcessDb> GetLast()
    {
        try
        {
            _logger.LogInformation("GetLast");

            await using var context = _serviceScopeFactory.CreateScope().ServiceProvider
                .GetRequiredService<S7plcSqlContext>();
            
            return context.Proces.OrderBy(x => x.ID).LastOrDefault();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "GetLast");
            return null;
        }
    }

    public async Task<ProcessDb[]> GetAll()
    {
        try
        {
            _logger.LogInformation("GetAll");

            await using var context = _serviceScopeFactory.CreateScope().ServiceProvider
                .GetRequiredService<S7plcSqlContext>();

            return context.Proces.ToArray();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "GetAll");
            return Array.Empty<ProcessDb>();
        }
    }

    public async Task<ProcessDb[]> GetByID(int id)
    {
        try
        {
            _logger.LogInformation("GetByID | {Index}", id);

            await using var context = _serviceScopeFactory.CreateScope().ServiceProvider
                .GetRequiredService<S7plcSqlContext>();

            return context.Proces.Where(x => x.ID >= id * 100000 && x.ID <= (id + 1) * 100000).ToArray();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "GetByID");
            return Array.Empty<ProcessDb>();
        }
    }

    public async Task<ProcessDb[]> GetWithFilter(Expression<Func<ProcessDb, bool>> predicate)
    {
        try
        {
            _logger.LogInformation("GetWithFilter | {Filter}", predicate.Body);

            await using var context = _serviceScopeFactory.CreateScope().ServiceProvider
                .GetRequiredService<S7plcSqlContext>();

            var query = context.Set<ProcessDb>().AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            return query.ToArray();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "GetWithFilter");
            return Array.Empty<ProcessDb>();
        }
    }
}