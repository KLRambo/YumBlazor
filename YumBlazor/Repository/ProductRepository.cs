using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class ProductRepository(ApplicationDbContext _db, IWebHostEnvironment webHostEnvironment) : IProductRepository
    {
        public async Task<Product> CreateAsync(Product obj)
        {
            _db.Product.Add(obj);
            await _db.SaveChangesAsync();

            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Product = await _db.Product.FirstOrDefaultAsync(c => c.Id == id);
            var imagePath = Path.Combine(webHostEnvironment.WebRootPath, Product.ImageUrl.TrimStart('/'));

            if (File.Exists(imagePath))
                File.Delete(imagePath);

            if (Product != null)
            {
                _db.Remove(Product);
                return await _db.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<Product> GetAsync(int id)
        {
            var obj = await _db.Product.FirstOrDefaultAsync(c => c.Id == id);
            if (obj == null)
            {
                return new Product();
            }

            return obj;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _db.Product.Include(u => u.Category).ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product obj)
        {
            var objFromDb = _db.Product.FirstOrDefault(c => c.Id == obj.Id);

            if(objFromDb is not null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
                objFromDb.ImageUrl = obj.ImageUrl;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.Price = obj.Price;

                _db.Product.Update(objFromDb);
                await _db.SaveChangesAsync();

                return objFromDb;
            }
            return obj;
        }
    }
}
