using FluentValidation.AspNetCore;
using MicroserviceBasedFintechApp.PaymentService.Api;
using MicroserviceBasedFintechApp.PaymentService.Api.Consumers;
using MicroserviceBasedFintechApp.PaymentService.Api.Workers;
using MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Infrastructure;
using MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Repository;
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
builder.Services.AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<Program>());


builder.Services.AddHostedService<RunMigrations>();

#region Services
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddHostedService<PaymentsConsumer>();
builder.Services.AddHostedService<PaymentStatusNotifier>();
builder.Services.AddSingleton<IPaymentDispatcherService, DispatcherService>();
builder.Services.AddSingleton<IHashService, Sha256HashService>();

#endregion

#region Persistence
builder.Services.AddSingleton<IRabbitMqInfrastructureWrapper, RabbitInfrastructureWrapper>();
builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection("RabbitConfig"));
builder.Services.AddDbContext<PaymentServiceDbContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddSingleton<IEventsPublisher,EventsPublisher>();
#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

app.Run();
