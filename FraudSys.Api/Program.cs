using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using FraudSys.Domain.Commands.v1.CreateAccountLimit;
using FraudSys.Domain.Interfaces.v1.Repositories;
using FraudSys.Domain.Queries.v1.GetAccountLimit;
using FraudSys.Infrastructure.Data.Dynamo.v1.Repositories;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAccountLimitCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAccountLimitQueryHandler).Assembly));

// Configuração do DynamoDB
builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
{
    var credentials = new BasicAWSCredentials("fake", "fake");

    var config = new AmazonDynamoDBConfig
    {
        ServiceURL = "http://localhost:8000"
    };

    return new AmazonDynamoDBClient(credentials, config);
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