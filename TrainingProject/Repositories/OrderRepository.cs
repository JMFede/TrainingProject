using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.OpenApi.Any;
using TrainingProject.Interfaces;
using TrainingProject.Models;

namespace TrainingProject.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public readonly IDbContextFactory<TrainingDatabaseContext> _databaseContextFactory;

        public OrderRepository(IDbContextFactory<TrainingDatabaseContext> dbContext)
        {
            _databaseContextFactory = dbContext;
        }

        public async Task<List<Order>> GetOrder()
        {
            using (var ctx = _databaseContextFactory.CreateDbContext())
            {
                var result = await ctx.Orders.Include(x => x.Status).Include(x => x.Line).Include(x => x.Type).Include(x => x.User)
                                    .Select(x => new Order
                                    {
                                        OrderId = x.OrderId,
                                        Name = x.Name,
                                        TypeId = x.TypeId,
                                        Type = x.Type,
                                        LineId = x.LineId,
                                        Line = x.Line,
                                        Batch = x.Batch,
                                        Quantity = x.Quantity,
                                        PlannedQuantity = x.PlannedQuantity,
                                        PlannedDate = x.PlannedDate,
                                        UserId = x.UserId,
                                        User = x.User,
                                        Wbs = x.Wbs,
                                        StartingDate = x.StartingDate,
                                        StatusId = x.StatusId,
                                        Status = x.Status

                                    }).ToListAsync();
                return result;
            }

        }
        public async Task<List<Models.Type>> GetType()
        {
            using (var ctx = _databaseContextFactory.CreateDbContext())
            {
                var typeNames = await ctx.Orders.Include(x => x.Type)
                .Select(selector: x => x.Type)
                .Distinct()
                .ToListAsync();

                return typeNames;
            }
        }
        public async Task<List<Line>> GetLine()
        {
            using (var ctx = _databaseContextFactory.CreateDbContext())
            {
                var lineNames = await ctx.Orders.Include(x => x.Line)
                .Select(selector: x => x.Line)
                .Distinct()
                .ToListAsync();

                return lineNames;
            }
                
        }
        public async Task<List<User>> GetUser()
        {
            using (var ctx = _databaseContextFactory.CreateDbContext())
            {
                var userNames = await ctx.Orders.Include(x => x.User)
                .Select(selector: x => x.User)
                .Distinct()
                .ToListAsync();

                return userNames;
            }
        }

        public async Task<List<OrderStatus>> GetStatus()
        {
            using (var ctx = _databaseContextFactory.CreateDbContext())
            {
                var statusNames = await ctx.Orders.Include(x => x.Status)
                .Select(selector: x => x.Status)
                .Distinct()
                .ToListAsync();

                return statusNames;
            }
        }

        public async Task<Order> AddOrder(Order order)
        {
            using (var ctx = _databaseContextFactory.CreateDbContext())
            {
                order.OrderId = GenerateUniqueOrderId();
                ctx.Orders.Add(order);
                await ctx.SaveChangesAsync();
                return order;
            }
        }

        public async Task<User> AddUser(User user)
        {
            using (var ctx = _databaseContextFactory.CreateDbContext())
            {
                var findUser = await ctx.Users.Where(x => x.UserName == user.UserName).FirstOrDefaultAsync();

                if (findUser == null)
                {
                    user.UserId = GenerateUniqueUserId();
                    ctx.Users.Add(user);
                    await ctx.SaveChangesAsync();
                    return user;
                }
                else
                {
                    return findUser;
                }
            }
        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            using (var ctx = _databaseContextFactory.CreateDbContext())
            {
                var order = await ctx.Orders.Where(x => x.OrderId == orderId).ExecuteDeleteAsync();
                return true;
            }
        }

        public async Task<Order> EditOrder(Order order)
        {
            using (var ctx = _databaseContextFactory.CreateDbContext())
            {
                if (order.Quantity >= order.PlannedQuantity)
                {
                    order.StatusId = 2;
                }

                var updatedOrder = await ctx.Orders.Where(x => x.OrderId == order.OrderId)
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(x => x.LineId, order.LineId)
                        .SetProperty(x => x.PlannedDate, order.PlannedDate)
                        .SetProperty(x => x.StartingDate, order.StartingDate)
                        .SetProperty(x => x.StatusId, order.StatusId)
                        .SetProperty(x => x.Quantity, order.Quantity)
                        .SetProperty(x => x.UserId, order.UserId));

                return order;
            }
        }

        public async Task<User> IsRegistered(string username)
        {
            using (var ctx = _databaseContextFactory.CreateDbContext())
            {
                var user = await ctx.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();

                if (user == null)
                {
                    return null;
                }

                return user;
            }
        }



        private int GenerateUniqueOrderId()
        {
            using (var ctx = _databaseContextFactory.CreateDbContext())
            {
                int newOrderId;

                do
                {
                    newOrderId = new Random().Next(1, 9999);
                } while (ctx.Orders.Any(o => o.OrderId == newOrderId));

                return newOrderId;
            }
        }

        private int GenerateUniqueUserId()
        {
            using (var ctx = _databaseContextFactory.CreateDbContext())
            {
                int newUserId;

                do
                {
                    newUserId = new Random().Next(1, 9999);
                } while (ctx.Users.Any(o => o.UserId == newUserId));

                return newUserId;
            }
        }
    }
}
