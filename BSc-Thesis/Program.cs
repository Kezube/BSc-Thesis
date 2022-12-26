using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using BSc_Thesis.Data;
using BSc_Thesis.DataBase;
using NLog;
using NLog.Web;

try
{
    var builder = WebApplication.CreateBuilder(args);

    var configuarion = builder.Configuration;
    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddSingleton<WeatherForecastService>();
    builder.Services.AddDatabase(configuarion);
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services
        .AddBlazorise(options =>
        {
            options.Immediate = true;
        })
        .AddBootstrap5Providers()
        .AddFontAwesomeIcons();
    var app = builder.Build();
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    app.Run();

}
catch (Exception)
{

    throw;
}