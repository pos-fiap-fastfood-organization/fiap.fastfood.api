
using Api.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
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
    ConfigureCore(services);
    ConfigureAdapter(services);
    ConfigureInfrastructure(services);
}

static void ConfigureCore(IServiceCollection services)
{
    services
        .AddUserCases();
}

static void ConfigureAdapter(IServiceCollection services)
{
    services
        .AddGateways()
        .AddCoreControllers()
        ;
}

static void ConfigureInfrastructure(IServiceCollection services)
{
    services
        .AddDatabases()
        ;
}