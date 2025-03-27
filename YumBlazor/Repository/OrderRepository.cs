using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class OrderRepository(ApplicationDbContext context) : IOrderRepository
    {
        public async Task<OrderHeader> CreateAsync(OrderHeader orderHeader)
        {
            orderHeader.OrderDate = DateTime.Now;
            await context.OrderHeader.AddAsync(orderHeader);
            await context.SaveChangesAsync();

            return orderHeader;
        }

        public async Task<OrderHeader> GetAsync(int id)
        {
            return await context.OrderHeader.Include(u => u.OrderDetails).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<OrderHeader>> GetAllASync(string? userId = null)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return await context.OrderHeader.Where(o => o.UserId == userId).ToListAsync();
            }
            return await context.OrderHeader.ToListAsync();
        }

        public async Task<OrderHeader> UpdateStatusAsync(int orderId, string status)
        {
            var orderHeader = await context.OrderHeader.FirstOrDefaultAsync(u => u.Id == orderId);

            if(orderHeader != null)
            {
                orderHeader.Status = status;

                await context.SaveChangesAsync();
            }

            return orderHeader;
        }
    }
}
