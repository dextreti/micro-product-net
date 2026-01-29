using Catalog.Order.API.Middleware;
using Catalog.Order.Application;
using Catalog.Order.Postgresql;
using Catalog.Order.Kafka;

var builder = WebApplication.CreateBuilder(args);


// 1. Sin esto, .NET no sabe cómo manejar [ApiController] ni las rutas
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddPersistenciaIoC(builder.Configuration); 
builder.Services.AddMessagingConfiguration(builder.Configuration);
builder.Services.AddApplicationIoC();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
// 2. Esto activa el mapeo de las rutas de los atributos [Route]
app.MapControllers();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.Run();

