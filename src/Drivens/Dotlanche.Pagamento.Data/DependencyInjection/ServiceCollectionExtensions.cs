using Dotlanche.Pagamento.Data.DatabaseContext;
using Dotlanche.Pagamento.Data.Repositories;
using Dotlanche.Pagamento.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dotlanche.Pagamento.Data.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgresqlDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<PagamentoDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IRegistroPagamentoRepository, RegistroPagamentoRepository>();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return services;
        }

        public static IServiceCollection RunDatabaseMigrations(this IServiceCollection services, IConfiguration configuration)
        {
            using (var sp = services.BuildServiceProvider())
            {
                using var scope = sp.CreateScope();

                var db = scope.ServiceProvider.GetRequiredService<PagamentoDbContext>();
                db.Database.Migrate();
            }

            return services;
        }
    }
}