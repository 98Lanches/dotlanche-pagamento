using Dotlanche.Pagamento.Data.DatabaseContext;
using Dotlanche.Pagamento.Data.Repositories;
using Dotlanche.Pagamento.Domain.Repositories;
using Dotlanche.Pagamento.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dotlanche.Pagamento.BDDTests.Setup
{
    public class PagamentoApi : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                SetupInMemoryDatabase(services);
            });
            builder.ConfigureAppConfiguration(cfgBuilder =>
            {
                cfgBuilder.AddEnvironmentVariables();

                var appsettingsFilePath = Path.Combine(Environment.CurrentDirectory, "appsettings.bdd.json");
                cfgBuilder.AddJsonFile(appsettingsFilePath);
            });

            builder.UseEnvironment("Development");
        }

        private static IServiceCollection SetupInMemoryDatabase(IServiceCollection services)
        {
            var dbContextDescriptor = services.Single(
                d => d.ServiceType ==
                    typeof(DbContextOptions<PagamentoDbContext>));

            services.Remove(dbContextDescriptor);

            // In memory database to simplify test run on CI
            services.AddDbContextPool<PagamentoDbContext>(options =>
                options.UseInMemoryDatabase("Pagamento"));
            services.AddScoped<IRegistroPagamentoRepository, RegistroPagamentoRepository>();

            return services;
        }
    }
}