using Microsoft.EntityFrameworkCore;
using RaremintShop.Core.Interfaces.Repositories;
using RaremintShop.Core.Models;
using RaremintShop.Infrastructure.Data;

namespace RaremintShop.Infrastructure.Repositories
{
    public class ProductImageRepository : BaseRepository<ProductImage>, IProductImageRepository
    {
        /// <summary>
        /// コンストラクタ。データベースコンテキストを受け取ります。
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        public ProductImageRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<int> GetMaxSortOrderAsync(int productId)
        {
            return await _dbSet
                .Where(pi => pi.ProductId == productId)
                .Select(pi => pi.SortOrder)
                .DefaultIfEmpty(0)
                .MaxAsync();
        }
    }
}
