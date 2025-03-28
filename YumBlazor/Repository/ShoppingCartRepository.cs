using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class ShoppingCartRepository(ApplicationDbContext _db) : IShoppingCartRepository
    {
        public async Task<bool> ClearCartAsync(string? userId)
        {
            var cartItems = await _db.ShoppingCart.Where(u => u.UserId == userId).ToListAsync();

            _db.RemoveRange(cartItems);

            return await _db.SaveChangesAsync() > 0;

        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId)
        {
            return await _db.ShoppingCart.Where(u => u.UserId == userId).Include(p => p.Product).ToListAsync();
        }

        public async Task<int> GetTotalCartCountAsync(string? userId)
        {

            int cartCount = 0;

            var cartItems = await _db.ShoppingCart.Where(u => u.UserId == userId).ToListAsync();
            foreach (var item in cartItems)
            {
                cartCount += item.Count;
            }
            return cartCount;

        }

        public async Task<bool> UpdateCartAsync(string userid, int productId, int updateBy)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return false;
            }

            var cart = await _db.ShoppingCart.FirstOrDefaultAsync(c => c.UserId == userid && c.ProductId == productId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userid,
                    ProductId = productId,
                    Count = updateBy
                };

                await _db.ShoppingCart.AddAsync(cart);
            }
            else
            {
                cart.Count += updateBy;

                if (cart.Count <= 0)
                {
                    _db.ShoppingCart.Remove(cart);
                }
            }
            return await _db.SaveChangesAsync() > 0;
        }
    }
}