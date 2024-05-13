using TrainingProject.Models;
using Microsoft.EntityFrameworkCore;
using TrainingProject.Interfaces;
using TrainingProject.Repositories;
using TrainingProject.Services;
using TrainingProject.Hubs;
using Type = TrainingProject.Models.Type;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

void CheckConnection()
{
    const string connectionString = "Server=mysql,1433;Database=TrainingDatabase;User=sa;Password=yourStrong(!)Password; TrustServerCertificate=True";
    int retries = 3;
    int delay = 1000; // 1 second
    int maxDelay = 60000; // 1 minute

    while (retries > 0)
    {
        try
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Successfully connected to the database!");
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect to the database: {ex.Message}");
            delay *= 2;
            delay = Math.Min(delay, maxDelay);
            Console.WriteLine($"Retrying in {delay / 1000} seconds...");
            System.Threading.Thread.Sleep(delay);
            retries--;
        }
    }

    if (retries == 0)
    {
        Console.WriteLine("Failed to connect to the database after multiple retries. Exiting...");
        Environment.Exit(1);
    }
}

//CheckConnection();

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

app.UseSwagger();
app.UseSwaggerUI();

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


