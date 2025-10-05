using Carter;

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
// Add services to the container
var app = builder.Build();
app.MapCarter();

app.UseCatalogModule()
   .UseBasketgModule()
   .UseOrderingModule();    



// Configure HTTP Requests 


app.Run();
