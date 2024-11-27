using Dotlanche.Pagamento.Application.DependencyInjection;
using Dotlanche.Pagamento.Checkout.DependencyInjection;
using Dotlanche.Pagamento.Data.DependencyInjection;
using Dotlanche.Pagamento.Integrations.DependencyInjection;
using Dotlanche.Pagamento.WebApi.Extensions;
using Serilog;
using System.Text.Json.Serialization;

namespace Dotlanche.Pagamento.WebApi;

public class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureLogging(builder.Configuration);
            builder.Services.AddFakeCheckoutProvider();
            builder.Services.AddPostgresqlDatabase(builder.Configuration);
            builder.Services.RunDatabaseMigrations(builder.Configuration);

            builder.Services.AddPedidosServiceIntegration(builder.Configuration);

            builder.Services.AddPagamentoUseCases();

            builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddControllers();
            builder.Services.ConfigureHealthChecks(builder.Configuration);
            builder.Services.ConfigureSwagger();

            var app = builder.Build();

            app.UseSerilogRequestLogging();

            app.MapHealthChecks("/health");

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}