using Carter;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shared.Exceptions.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration));

builder.Services.AddCarter(configurator: config =>
{
   var catagoryModules = typeof(CatalogModule).Assembly.GetTypes()
                          .Where(t =>t.IsAssignableTo(typeof(ICarterModule))).ToArray();

    config.WithModules(catagoryModules);
});


builder.Services
    .AddCatalogModule(builder.Configuration)
   .AddBasketModule(builder.Configuration)
   .AddOrderingModule(builder.Configuration);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();


var app = builder.Build();

app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(options => { });

app.UseCatalogModule()  
   .UseBasketgModule()
   .UseOrderingModule();





app.Run();
