using BSc_Thesis.DataBase.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BSc_Thesis.DataBase
{
    public static class Registration
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddSingleton<ProcessStore>();
            services.AddDbContext<S7plcSqlContext>((sp, options) =>
            {
                options.UseSqlServer("Server=127.0.0.1,1433;database=S7PLC_SQL;Persist Security Info=True;User ID=sa;password=admin!; TrustServerCertificate=True");
            });
            services.BuildServiceProvider().GetRequiredService<ProcessStore>();
            return services;
        }
    }
}
