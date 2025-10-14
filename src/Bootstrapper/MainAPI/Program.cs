using Carter;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions.Handlers;

var builder = WebApplication.CreateBuilder(args);

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
// Add services to the container
var app = builder.Build();

//app.UseExceptionHandler(exceptionHandler =>
//{
//    exceptionHandler.Run(async context =>
//    {
//      var exception  = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//        if(exception == null)
//        {
//            return;
//        }
//        var problem = new ProblemDetails
//        {
//            Title = exception.Message,
//            Detail = exception.StackTrace,
//            Status = 500
//        };
//        var logger  = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exception, exception.Message);

//        context.Response.StatusCode = 500;
//        context.Response.ContentType = "application/problem+json";

//        await context.Response.WriteAsJsonAsync(problem);
//    });

//});

app.MapCarter();

app.UseCatalogModule()
   .UseBasketgModule()
   .UseOrderingModule();



// Configure HTTP Requests 
app.UseExceptionHandler(options => { });



app.Run();
