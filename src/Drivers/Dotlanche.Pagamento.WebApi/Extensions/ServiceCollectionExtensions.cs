using Dotlanche.Pagamento.Application.Exceptions;
using Microsoft.OpenApi.Models;
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
    }
}