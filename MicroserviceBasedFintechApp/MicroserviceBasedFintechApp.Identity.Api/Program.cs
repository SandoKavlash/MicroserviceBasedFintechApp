using MicroserviceBasedFintechApp.Identity.Api.Consumers;
using MicroserviceBasedFintechApp.Identity.Api.Extensions;
using MicroserviceBasedFintechApp.Identity.Persistence.DbContexts;
//TODO: docker run --name my_postgres_db -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=fintechapp -e POSTGRES_DB=fintech_app_db -p 1234:5432 -d postgres
//TODO: docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management
var builder = WebApplication.CreateBuilder(args);


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