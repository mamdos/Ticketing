using Data.Common.IoC;
using Data.Persistence.DbContexts;
using Data.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Services.Common.IoC;
using Services.Common.Models;
using WebApi.Common.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDataLayer();
builder.Services.AddServicesLayer(builder.Configuration);
builder.Services.AddApiLayer(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    context.Database.Migrate();

    var seedUserPassword = builder.Configuration.GetValue<string>("SeedSuperUserPassword");

    await UserSeeder.Initialize(services, seedUserPassword ?? "");
}

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
