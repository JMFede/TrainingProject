using TrainingProject.Models;
using Microsoft.EntityFrameworkCore;
using TrainingProject.Interfaces;
using TrainingProject.Repositories;
using TrainingProject.Services;
using TrainingProject.Hubs;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContextFactory<TrainingDatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<PlantService>();
builder.Services.AddHostedService<PlantService>(sp => sp.GetRequiredService<PlantService>());

builder.Services.AddSignalR();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200"));

app.UseAuthorization();

app.MapControllers();
app.MapHub<PlantHub>("/planthub");

app.Run();


