using MyDataAccess;
using MyServices;

namespace MyBusiness
{
    public class OrderService
    {
        private readonly Repository<Order> repository;
        private readonly ILoggerService logger;

        public OrderService(Repository<Order> repo, ILoggerService logger)
        {
            this.repository = repo;
            this.logger = logger;
        }

        public void ProcessOrder(Order order)
        {
            repository.Add(order);
            logger.Log($"Zamówienie #{order.OrderId} dla {order.CustomerName} zostało przetworzone.");
        }
    }
}
