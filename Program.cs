using ClashDetectionServer.Interfaces;
using ClashDetectionServer.Managers;
using ClashDetectionServer.Rules.Global;
using ClashDetectionServer.Rules.Zoning;
using ClashDetectionServer.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IClashDetectionManager, ClashDetectionManager>();
builder.Services.AddScoped<IGlobalViolationRule, InsideSiteBoundaryRule>();
builder.Services.AddScoped<IGlobalViolationRule, NoOverlapRule>();
builder.Services.AddScoped<IGlobalViolationRule, MinimumClearanceRule>();
builder.Services.AddScoped<IZoningViolationRule, NightclubSchoolDistanceRule>();
builder.Services.AddScoped<IZoningViolationRule, ResidentialDistanceRule>();
builder.Services.AddScoped<ISpatialIndexService, BruteForceSpatialIndexService>();
// builder.Services.AddScoped<ISpatialIndexService, UniformGridSpatialIndexService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
