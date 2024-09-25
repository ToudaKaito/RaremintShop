using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RaremintShop.Module.Catalog.Data;
using RaremintShop.Module.Catalog.Models;
using RaremintShop.Module.Catalog.Repositories;

namespace RaremintShop.Infrastructure.Repositories
{
    /// <summary>
    /// 商品情報を管理するリポジトリの実装クラス
    /// </summary>
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


        /// <summary>
        /// 商品IDに基づいて商品を非同期的に取得します。
        /// </summary>
        /// <param name="productId">取得する商品のID</param>
        /// <returns>商品エンティティ</returns>
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId).ConfigureAwait(false);
            if (product == null)
            {
                throw new KeyNotFoundException($"商品ID {productId} の商品が見つかりませんでした。");
            }
            return product;
        }

        /// <summary>
        /// キーワードとカテゴリーに基づいて商品を非同期的に検索します。
        /// </summary>
        /// <param name="keyword">検索するキーワード</param>
        /// <param name="category">検索するカテゴリー</param>
        /// <returns>商品のリスト</returns>
        public async Task<IEnumerable<Product>> SearchProductsAsync(string keyword, string category)
        {
            return await _context.Products
                .Where(p => (string.IsNullOrEmpty(keyword) || p.ProductName.Contains(keyword))
                && (string.IsNullOrEmpty(category) || p.Category == category))
                .ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 新しい商品を非同期的に追加します。
        /// </summary>
        /// <param name="product">追加する商品エンティティ</param>
        public async Task AddProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));

            }

            _context.Products.Add(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex, "商品の追加", product.ProductID);
            }
        }

        /// <summary>
        /// 既存の商品情報を非同期的に更新します。
        /// </summary>
        /// <param name="product">更新する商品エンティティ</param>
        public async Task UpdateProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _context.Products.Update(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex, "商品の更新", product.ProductID);
            }
        }
    }
}
