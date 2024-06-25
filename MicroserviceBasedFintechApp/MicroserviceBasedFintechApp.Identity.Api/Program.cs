using MicroserviceBasedFintechApp.Identity.Api;
using MicroserviceBasedFintechApp.Identity.Api.Consumers;
using MicroserviceBasedFintechApp.Identity.Api.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<RunMigrations>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();


builder.Services.AddCore();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddHostedService<OrderAuthenticationConsumer>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

app.Run();