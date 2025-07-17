
using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ConfigureServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsProduction() is false)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static void ConfigureServices(IServiceCollection services)
{
    ConfigureInfrastructure(services);
    ConfigureCore(services);
}

static void ConfigureCore(IServiceCollection services)
{
    services
        .AddUserCases()
        .AddCoreControllers();
}

static void ConfigureInfrastructure(IServiceCollection services)
{
    services
        .AddDatabases()
        .AddGateways();
}