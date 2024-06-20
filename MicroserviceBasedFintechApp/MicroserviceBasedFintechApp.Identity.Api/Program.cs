using MicroserviceBasedFintechApp.Identity.Api.Extensions;
using MicroserviceBasedFintechApp.Identity.Persistence.DbContexts;
//TODO: docker run --name my_postgres_db -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=fintechapp -e POSTGRES_DB=fintech_app_db -p 1234:5432 -d postgres
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();


builder.Services.AddCore();
builder.Services.AddPersistence();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

app.Run();