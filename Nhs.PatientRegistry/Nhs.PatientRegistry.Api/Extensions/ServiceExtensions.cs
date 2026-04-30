using Asp.Versioning;
using FluentValidation;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi;
using Nhs.PatientRegistry.Api.Abstractions;
using Nhs.PatientRegistry.Api.Repositories;
using Nhs.PatientRegistry.Api.Services;
using Nhs.PatientRegistry.Api.Validation;

namespace Nhs.PatientRegistry.Api.Extensions
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IPatientRepository, InMemoryPatientRepository>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IValidator<int>, PatientIdValidator>();
            return services;
        }
        public static IServiceCollection AddApiVersioningConfiguration(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }


        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Patient Registry API",
                    Version = "v1",
                    Description = "API for retrieving patient summaries. ",
                });
            });
            return services;
        }

        public static IServiceCollection AddHealthCheckConfiguration(this IServiceCollection services)
        {

            services.AddHealthChecks()
                .AddCheck("Nhs.PatientRegistry.Api", () =>
                    HealthCheckResult.Healthy("API is accepting requests"));

            return services;
        }
    }
}
