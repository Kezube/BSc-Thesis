using BSc_Thesis.DataBase.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BSc_Thesis.DataBase.Stores
{
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

                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();
                return context.Proces.ToArray()[^1];
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetLastRow");
                return Array.Empty<ProcessDb>()[^1];

            }
        }

        public async Task<ProcessDb[]> GetAllRows()
        {
            try
            {
                _logger.LogInformation("GetAllRows");

                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();

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
                _logger.LogInformation("GetAllRowsByID");

                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();

                return context.Proces.Where(x => x.ID >= index*100000 && x.ID <= (index+1)*100000).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllRowsByID");
                return Array.Empty<ProcessDb>();

            }
        }

        public async Task<ProcessDb[]> GetAllRowsByDate(DateTime? dateTimeFrom, DateTime? dateTimeTo)
        {
            try
            {
                _logger.LogInformation("GetAllRowsByID");

                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();

                return context.Proces.Where(x => x.Date >= dateTimeFrom && x.Date <= dateTimeTo).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllRowsByID");
                return Array.Empty<ProcessDb>();

            }
        }

        public async Task<ProcessDb[]> GetAllRowsByTemp(double lowLimit,double highLimit)
        {
            try
            {
                _logger.LogInformation("GetAllRowsByTemp");

                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();

                return context.Proces.Where(x => x.Temperature >= lowLimit && x.Temperature <= highLimit).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllRowsByTemp");
                return Array.Empty<ProcessDb>();

            }
        }

        public async Task<ProcessDb[]> GetAllRowsByAmbinetTemp(double lowLimit, double highLimit)
        {
            try
            {
                _logger.LogInformation("GetAllRowsByAmbinetTemp");

                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();

                return context.Proces.Where(x => x.AmbientTemperature >= lowLimit && x.AmbientTemperature <= highLimit).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllRowsByAmbinetTemp");
                return Array.Empty<ProcessDb>();

            }
        }

        public async Task<ProcessDb[]> GetAllRowsByGlucose(double lowLimit, double highLimit)
        {
            try
            {
                _logger.LogInformation("GetAllRowsByGlucose");

                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();

                return context.Proces.Where(x => x.Glucose >= lowLimit && x.Glucose <= highLimit).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllRowsByGlucose");
                return Array.Empty<ProcessDb>();

            }
        }

        public async Task<ProcessDb[]> GetAllRowsByMaltose(double lowLimit, double highLimit)
        {
            try
            {
                _logger.LogInformation("GetAllRowsByMaltose");

                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();

                return context.Proces.Where(x => x.Maltose >= lowLimit && x.Maltose <= highLimit).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllRowsByMaltose");
                return Array.Empty<ProcessDb>();

            }
        }

        public async Task<ProcessDb[]> GetAllRowsByMaltotriosis(double lowLimit, double highLimit)
        {
            try
            {
                _logger.LogInformation("GetAllRowsByMaltotriosis");

                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();

                return context.Proces.Where(x => x.Maltotriosis >= lowLimit && x.Maltotriosis <= highLimit).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllRowsByMaltotriosis");
                return Array.Empty<ProcessDb>();

            }
        }

        public async Task<ProcessDb[]> GetAllRowsBySugar(double lowLimit, double highLimit)
        {
            try
            {
                _logger.LogInformation("GetAllRowsBySugar");

                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();

                return context.Proces.Where(x => x.Sugar >= lowLimit && x.Sugar <= highLimit).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllRowsBySugar");
                return Array.Empty<ProcessDb>();

            }
        }

        public async Task<ProcessDb[]> GetAllRowsByDeadYeast(double lowLimit, double highLimit)
        {
            try
            {
                _logger.LogInformation("GetAllRowsByDeadYeast");

                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();

                return context.Proces.Where(x => x.DeadYeast >= lowLimit && x.DeadYeast <= highLimit).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllRowsByDeadYeast");
                return Array.Empty<ProcessDb>();

            }
        }

        public async Task<ProcessDb[]> GetAllRowsByActiveYeast(double lowLimit, double highLimit)
        {
            try
            {
                _logger.LogInformation("GetAllRowsByActiveYeast");

                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();

                return context.Proces.Where(x => x.ActiveYeast >= lowLimit && x.ActiveYeast <= highLimit).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllRowsByActiveYeast");
                return Array.Empty<ProcessDb>();

            }
        }

        public async Task<ProcessDb[]> GetAllRowsByLatticeYeast(double lowLimit, double highLimit)
        {
            try
            {
                _logger.LogInformation("GetAllRowsByLatticeYeast");

                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();

                return context.Proces.Where(x => x.LatticeYeast >= lowLimit && x.LatticeYeast <= highLimit).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllRowsByLatticeYeast");
                return Array.Empty<ProcessDb>();

            }
        }

        public async Task<ProcessDb[]> GetAllRowsByEthanol(double lowLimit, double highLimit)
        {
            try
            {
                _logger.LogInformation("GetAllRowsByEthanol");

                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();

                return context.Proces.Where(x => x.Ethanol >= lowLimit && x.Ethanol <= highLimit).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllRowsByEthanol");
                return Array.Empty<ProcessDb>();

            }
        }

    }
}
