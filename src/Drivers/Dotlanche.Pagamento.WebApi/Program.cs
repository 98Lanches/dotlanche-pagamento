using Dotlanche.Pagamento.Application.DependencyInjection;
using Dotlanche.Pagamento.Checkout.DependencyInjection;
using Dotlanche.Pagamento.Data.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFakeCheckoutProvider();
builder.Services.AddPostgresqlDatabase(builder.Configuration);
builder.Services.AddPagamentoUseCases();

builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
