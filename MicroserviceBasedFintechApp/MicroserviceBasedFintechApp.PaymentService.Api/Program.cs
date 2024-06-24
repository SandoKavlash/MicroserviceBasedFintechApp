using MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Services;
using MicroserviceBasedFintechApp.PaymentService.Core.Implementations;
using MicroserviceBasedFintechApp.PaymentService.Persistence.Abstraction;
using MicroserviceBasedFintechApp.PaymentService.Persistence.Contacts.Configs;
using MicroserviceBasedFintechApp.PaymentService.Persistence.DbContexts;
using MicroserviceBasedFintechApp.PaymentService.Persistence.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGen();

#region Services
builder.Services.AddScoped<IPaymentService, PaymentService>();


#endregion

#region Persistence
builder.Services.AddSingleton<IRabbitMqInfrastructureWrapper, RabbitInfrastructureWrapper>();
builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection("RabbitConfig"));
builder.Services.AddDbContext<PaymentServiceDbContext>();

#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

app.Run();
