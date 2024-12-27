using Infra.Config;
using Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoConfig>(
        builder.Configuration.GetSection("ReservationDatabase"));

builder.Services.AddSingleton<ReservationServices>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
            options.JsonSerializerOptions.PropertyNamingPolicy = null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();

