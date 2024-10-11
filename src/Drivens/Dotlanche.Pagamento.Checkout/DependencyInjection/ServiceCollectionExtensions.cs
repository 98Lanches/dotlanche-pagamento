using Dotlanche.Pagamento.Application.Ports;
using Dotlanche.Pagamento.Checkout.Adapters;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Dotlanche.Pagamento.Checkout.DependencyInjection
{
    [ExcludeFromCodeCoverage(Justification = "DI Class with no business logic")]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFakeCheckoutProvider(this IServiceCollection services)
        {
            services.AddScoped<IQrCodeProvider, FakeCheckoutQrCodeProvider>();

            return services;
        }
    }
}
