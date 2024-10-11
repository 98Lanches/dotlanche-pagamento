using Dotlanche.Pagamento.Checkout.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFakeCheckoutProvider();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
