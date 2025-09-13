
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCatalogModule(builder.Configuration)
   .AddBasketModule(builder.Configuration)
   .AddOrderingModule(builder.Configuration);
// Add services to the container
var app = builder.Build();
app.UseCatalogModule()
   .UseBasketgModule()
   .UseOrderingModule();    



// Configure HTTP Requests 


app.Run();
