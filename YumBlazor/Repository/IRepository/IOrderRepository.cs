using YumBlazor.Data;

namespace YumBlazor.Repository.IRepository
{
    public interface IOrderRepository
    {
        Task<OrderHeader> CreateAsync(OrderHeader orderHeader);
        Task<OrderHeader> GetAsync(int id);
        Task<IEnumerable<OrderHeader>> GetAllAsync(string? userId =null);
        Task<OrderHeader> UpdateStatusAsync(int orderId, string status);


    }
}
