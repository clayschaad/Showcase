using Showcase.Domain;
using Showcase.Domain.Measurements;
using Showcase.Domain.Measurements.Temperatures;
using Showcase.Infrastructure.Measurement;
using Showcase.Infrastructure.Persistence.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(typeof(ITemperatureService), typeof(TemperatureService));
builder.Services.AddTransient(typeof(ITemperatureMeasurement), typeof(TemperatureMeasurement));
builder.Services.AddTransient(typeof(ITemperaturePersistance), typeof(TemperaturePersistance));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
