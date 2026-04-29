using Asp.Versioning;
using Microsoft.OpenApi;

namespace Nhs.PatientRegistry.Api.Extensions
{
    public static class ServiceExtensions
    {
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
    }
}
