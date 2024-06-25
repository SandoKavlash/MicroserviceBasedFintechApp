using MicroserviceBasedFintechApp.OrderService.Api.Consumer;
using MicroserviceBasedFintechApp.OrderService.Api.Extensions;
using MicroserviceBasedFintechApp.OrderService.Api.Middlewares;
using MicroserviceBasedFintechApp.OrderService.Api.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services
    .AddCore()
    .AddPersistence(builder.Configuration)
    .AddHostedService<OrdersAuthenticatorJob>()
    .AddHostedService<AuthenticatedOrdersConsumer>()
    .AddHostedService<OrderStatusConsumer>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<DomainErrorHandlerMiddleware>();

app.MapControllers();


app.Run();