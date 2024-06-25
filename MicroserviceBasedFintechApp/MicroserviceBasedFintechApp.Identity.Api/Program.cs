using MicroserviceBasedFintechApp.Identity.Api.Consumers;
using MicroserviceBasedFintechApp.Identity.Api.Extensions;
using MicroserviceBasedFintechApp.Identity.Persistence.DbContexts;


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