var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<PricingService>();

var app = builder.Build();

app.MapControllers();
app.Run();
