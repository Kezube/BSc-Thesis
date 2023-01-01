using BSc_Thesis.DataBase.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BSc_Thesis.DataBase;

public static class Registration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MyConnection");

        services.AddSingleton<IProcessStore, ProcessStore>();
        services.AddHostedService<ProcessMonitor>();
        services.AddDbContext<S7plcSqlContext>((sp, options) => { options.UseSqlServer(connectionString); });

        return services;
    }
}