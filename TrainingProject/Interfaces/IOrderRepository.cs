using TrainingProject.Models;

namespace TrainingProject.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrder();
        Task<List<Models.Type>> GetType();
        Task<List<Line>> GetLine();
        Task<List<User>> GetUser();
        Task<List<OrderStatus>> GetStatus();
        Task<Order> AddOrder(Order order);
        Task<User> AddUser(User user);
        Task<Order> EditOrder(Order order);
        Task<bool> DeleteOrder(int id);
        Task<User> IsRegistered(string username);
    }
}
