using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RaremintShop.Module.Catalog.Data;
using RaremintShop.Module.Catalog.Models;
using RaremintShop.Module.Catalog.Repositories;

namespace RaremintShop.Infrastructure.Repositories
{
    ///// <summary>
    ///// 商品情報を管理するリポジトリの実装クラス
    ///// </summary>
    public class ProductRepository : BaseRepository<CatalogDbContext>, IProductRepository
    {
        /// <summary>
        /// コンストラクタ。データベースコンテキストとロガーを受け取ります。
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        /// <param name="logger">ロガーインスタンス</param>
        public ProductRepository(CatalogDbContext context, ILogger<ProductRepository> logger)
            : base(context, logger)
        {
        }


     


        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch
            {
                throw;
            }
                
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
    }
}
