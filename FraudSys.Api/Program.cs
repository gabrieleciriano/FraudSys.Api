using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using FraudSys.Domain.Commands.v1.CreateAccountLimit;
using FraudSys.Domain.Interfaces.v1.Repositories;
using FraudSys.Infrastructure.Data.Dynamo.v1.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAccountLimitCommandHandler).Assembly));

// Configuração do DynamoDB
builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
{
    var config = new AmazonDynamoDBConfig
    {
        ServiceURL = "http://localhost:8000", 
        RegionEndpoint = Amazon.RegionEndpoint.USEast1
    };
    return new AmazonDynamoDBClient(config);
});

builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();

builder.Services.AddScoped<IAccountLimitRepository, AccountLimitRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();