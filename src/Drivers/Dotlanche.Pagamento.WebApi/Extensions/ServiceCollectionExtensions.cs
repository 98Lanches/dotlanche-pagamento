using Dotlanche.Pagamento.Application.Exceptions;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using System.Reflection;

namespace Dotlanche.Pagamento.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DotLanches Pagamento API",
                    Description = "Serviço de gerenciamento de pagamentos Dotlanches"
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return services;
        }

        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("DefaultConnection") ??
                    throw new MisconfigurationException("ConnectionStrings.DefaultConnection"));

            return services;
        }

        public static IServiceCollection ConfigureLogging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSerilog();

            return services;
        }

        public static IServiceCollection ConfigureOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            var tracingOtlpEndpoint = configuration["OpenTelemetry:ProviderEndpointUrl"];
            var otel = services.AddOpenTelemetry();

            otel.ConfigureResource(resource => resource.AddService(serviceName: "Pagamentos Api"));

            otel.WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddMeter("Microsoft.AspNetCore.Hosting")
                .AddMeter("Microsoft.AspNetCore.Server.Kestrel"));

            otel.WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation();
                tracing.AddHttpClientInstrumentation();
                if (tracingOtlpEndpoint != null)
                {
                    tracing.AddOtlpExporter(otlpOptions =>
                     {
                         otlpOptions.Endpoint = new Uri(tracingOtlpEndpoint);
                     });
                }
            });

            return services;
        }
    }
}