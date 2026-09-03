using CorrelationId;
using FluentValidation;
using NttBank.RagSearch.Api.Endpoints.Rag;
using NttBank.RagSearch.Api.Middlewares;
using NttBank.RagSearch.Infrastructure.Configurations;
using NttBank.RagSearch.Infrastructure.Extensions;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddAppConfigs(builder.Configuration);

var appConfiguration = builder.Configuration
    .Get<AppConfiguration>()!;

builder.AddLogger(appConfiguration);

builder.Services
    .AddDatabase(appConfiguration, builder.Environment)
    .AddEmbeddingGenerator(appConfiguration)
    .AddApplicationServices()
    .AddMapper()
    .AddOpenTelemetry(appConfiguration)
    .AddProblemDetails()
    .AddExceptionHandler<CustomExceptionHandler>()
    .AddValidatorsFromAssembly(typeof(Program).Assembly)
    .AddCorrelation()
    .AddHealthCheck();

var app = builder.Build();

app.UseCorrelationId();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseOpenTelemetryPrometheusScrapingEndpoint("metrics");
app.AddHealthCheckEndpoints();
app.MapRagEndpoints();

await app.RunAsync();
