using Dotlanche.Pagamento.Application.DependencyInjection;
using Dotlanche.Pagamento.Application.Exceptions;
using Dotlanche.Pagamento.Checkout.DependencyInjection;
using Dotlanche.Pagamento.Data.DependencyInjection;
using Dotlanche.Pagamento.Integrations.DependencyInjection;
using System.Text.Json.Serialization;

namespace Dotlanche.Pagamento.WebApi;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddFakeCheckoutProvider();

        builder.Services.AddPostgresqlDatabase(builder.Configuration);
        builder.Services.RunDatabaseMigrations(builder.Configuration);

        builder.Services.AddPedidosServiceIntegration(builder.Configuration);

        builder.Services.AddPagamentoUseCases();

        builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services
            .AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection") ??
                throw new MisconfigurationException("ConnectionStrings.DefaultConnection"));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.MapHealthChecks("/health");

        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapControllers();

        app.Run();
    }
}