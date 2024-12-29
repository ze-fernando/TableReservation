using API.Infra.Config;
using API.Services;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Restaurant Reservation API",
        Description = "API para gerenciamento de reservas de restaurante usando MongoDB e Redis.",
    });
});

builder.Services.Configure<MongoConfig>(
        builder.Configuration.GetSection("ReservationDatabase"));

builder.Services.AddSingleton<ReservationService>();
builder.Services.AddSingleton<EmailService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
            options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddSingleton(new RedisConfig(
    builder.Configuration.GetSection("Redis")["ConnectionString"]));

builder.Services.AddSingleton<RedisService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant Reservation API v1");
        options.RoutePrefix = string.Empty; // Swagger será exibido na raiz (http://localhost:5000/)
    });
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
