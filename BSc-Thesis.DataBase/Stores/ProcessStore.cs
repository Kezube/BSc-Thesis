using BSc_Thesis.DataBase.Models;
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

        public async Task<ProcessDb[]> GetAllRows()
        {
            try
            {
                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();

                return context.Proces.ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllRows");
                return Array.Empty<ProcessDb>();

            }
        }

        public async Task<ProcessDb[]> GetProcessLessThenTempValue(double Value)
        {
            try
            {
                await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<S7plcSqlContext>();

                return context.Proces.Where(x => x.Temperatura < Value).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllRows");
                return Array.Empty<ProcessDb>();

            }
        }


    }
}
