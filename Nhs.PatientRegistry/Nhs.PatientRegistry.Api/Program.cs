using Nhs.PatientRegistry.Api.Extensions;
using Nhs.PatientRegistry.Api.Mapping;
using Nhs.PatientRegistry.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// ---Configure Automapper ---
builder.Services.AddAutoMapper(configuration =>
{
    configuration.AddProfile<PatientMappingProfile>();
});

// --- Register Application Services, Repositories and Validators ---
builder.Services.AddApplicationServices();

//--- API Versioning Configuration ---
builder.Services.AddApiVersioningConfiguration();

// --- API Documentation (Swagger) ---
builder.Services.AddSwaggerConfiguration();

//--Healthcheck Configuration --
builder.Services.AddHealthCheckConfiguration();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "Patient Registry API v1");
    });
}

app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = (context, report) =>
        context.Response.WriteAsJsonAsync(new
        {
            status = report.Status.ToString(),
            service = "Nhs.PatientRegistry.Api",
            timestamp = DateTime.UtcNow,
            version = "1.0.0"
        })
});

app.UseHttpsRedirection();



app.MapControllers();

app.Run();

public partial class Program { }