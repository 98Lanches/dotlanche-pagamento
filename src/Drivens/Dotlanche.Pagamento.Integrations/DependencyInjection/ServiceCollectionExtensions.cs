using Dotlanche.Pagamento.Application.Exceptions;
using Dotlanche.Pagamento.Application.Ports;
using Dotlanche.Pagamento.Integrations.Adapters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dotlanche.Pagamento.Integrations.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPedidosServiceIntegration(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IPedidosServiceClient, PedidosServiceClient>();
            services.AddHttpClient<IPedidosServiceClient, PedidosServiceClient>(client =>
            {
                client.BaseAddress = new Uri(config["Integrations:PedidoService:BaseAddress"] ??
                    throw new MisconfigurationException("Integrations:PedidoService:BaseAddress"));
            });

            return services;
        }
    }
}