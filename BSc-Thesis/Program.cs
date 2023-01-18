using System.Reflection;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using BSc_Thesis.DataBase;
using MediatR;
using MediatR.Courier.DependencyInjection;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

var configuarion = builder.Configuration;
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDatabase(configuarion);


var files = Directory
    .EnumerateFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "BSc-Thesis.*.dll",
        SearchOption.AllDirectories)
    .ToList();
var assemblies = files.Select(Assembly.LoadFrom).ToList();
assemblies.Add(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(assemblies.ToArray());
builder.Services.AddCourier(assemblies.ToArray());


builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services
    .AddBlazorise(options => { options.Immediate = true; })
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