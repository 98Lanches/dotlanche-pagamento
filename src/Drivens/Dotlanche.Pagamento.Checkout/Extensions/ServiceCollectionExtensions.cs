using Dotlanche.Pagamento.Application.Ports;
using Dotlanche.Pagamento.Checkout.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace Dotlanche.Pagamento.Checkout.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFakeCheckoutProvider(this IServiceCollection services)
        {
            services.AddScoped<ICheckoutProvider, FakeCheckoutProvider>();

            return services;
        }
    }
}
