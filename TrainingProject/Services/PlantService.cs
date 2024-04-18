using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrainingProject.Hubs;
using TrainingProject.Models;

namespace TrainingProject.Services
{
    public class PlantService: BackgroundService
    {
        private readonly IHubContext<PlantHub> _hubContext;
        private readonly Random _random;
        public readonly IDbContextFactory<TrainingDatabaseContext> _databaseContextFactory;

        public PlantService(IHubContext<PlantHub> hubContext, IDbContextFactory<TrainingDatabaseContext> dbContext)//  TrainingDatabaseContext databaseContext
        {
            _hubContext = hubContext;
            _random = new Random();
            _databaseContextFactory = dbContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                UpdateOrders();

                var delaySeconds = 60 + _random.Next(-10, 11);
                await Task.Delay(TimeSpan.FromSeconds(delaySeconds), stoppingToken);
            }
        }

        private void UpdateOrders()
        {
            using (var ctx = _databaseContextFactory.CreateDbContext())
            {
                foreach (var order in ctx.Orders.Where(o => o.StatusId == 1))
                {
                    order.Quantity++;
                    if (order.Quantity >= order.PlannedQuantity)
                    {
                        order.StatusId = 2;
                    }
                      
                    _hubContext.Clients.All.SendAsync("OrderUpdated", order.OrderId);
                    Console.WriteLine($"Order {order.OrderId} updated to {order.Quantity}");
                }
                ctx.SaveChanges();
            }

        }
    }
}
