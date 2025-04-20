using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RaremintShop.Module.Catalog.Data;
using RaremintShop.Module.Catalog.Models;
using RaremintShop.Module.Catalog.Repositories;

namespace RaremintShop.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<CatalogDbContext>, ICategoryRepository
    {
        /// <summary>
        /// コンストラクタ。データベースコンテキストとロガーを受け取ります。
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        /// <param name="logger">ロガーインスタンス</param>
        public CategoryRepository(CatalogDbContext context, ILogger<CategoryRepository> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return await _context.Categories.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> RegisterCategoryAsync(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                throw;
            }
        }
    }
}
