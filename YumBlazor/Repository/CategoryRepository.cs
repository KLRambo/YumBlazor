using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class CategoryRepository(ApplicationDbContext _db) : ICategoryRepository
    {
        public async Task<Category> CreateAsync(Category obj)
        {
            _db.Category.Add(obj);
            await _db.SaveChangesAsync();

            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _db.Category.FirstOrDefaultAsync(c => c.Id == id);

            if (category != null)
            {
                _db.Remove(category);
                return await _db.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<Category> GetAsync(int id)
        {
            var obj = await _db.Category.FirstOrDefaultAsync(c => c.Id == id);
            if (obj == null)
            {
                return new Category();
            }

            return obj;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Category.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category obj)
        {
            var objFromDb = _db.Category.FirstOrDefault(c => c.Id == obj.Id);

            if(objFromDb is not null)
            {
                objFromDb.Name = obj.Name;
                _db.Category.Update(objFromDb);
                await _db.SaveChangesAsync();

                return objFromDb;
            }
            return obj;
        }
    }
}
