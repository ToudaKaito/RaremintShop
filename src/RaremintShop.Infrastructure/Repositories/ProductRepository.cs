using Microsoft.EntityFrameworkCore;
using RaremintShop.Module.Catalog.Data;
using RaremintShop.Module.Catalog.Models;
using RaremintShop.Module.Catalog.Repositories;

namespace RaremintShop.Infrastructure.Repositories
{
    /// <summary>
    /// 商品情報を管理するリポジトリの実装クラス
    /// </summary>
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        /// <summary>
        /// コンストラクタ。データベースコンテキストを受け取ります。
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        public ProductRepository(CatalogDbContext context) : base(context)
        {
        }


        /// <summary>
        /// 商品画像を登録します。
        /// </summary>
        /// <param name="images">登録する画像のリスト</param>
        /// <returns>登録成功の場合は true</returns>
        public async Task<bool> RegisterProductImagesAsync(List<ProductImage> images)
        {
            try
            {
                _context.Set<ProductImage>().AddRange(images);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 指定された商品IDに関連付けられた画像の最大並び順を取得します。
        /// </summary>
        /// <param name="productId">商品ID</param>
        /// <returns>最大並び順（画像が存在しない場合は 0）</returns>
        public async Task<int> GetMaxSortOrderAsync(int productId)
        {
            try
            {
                return await _context.Set<ProductImage>()
                    .Where(pi => pi.ProductId == productId)
                    .Select(pi => pi.SortOrder)
                    .DefaultIfEmpty(0)
                    .MaxAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}