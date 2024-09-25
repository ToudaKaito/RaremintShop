using RaremintShop.Module.Catalog.Models;
using RaremintShop.Module.Catalog.Repositories;
using System.Threading.Tasks;

namespace RaremintShop.Module.Catalog.Services
{
    /// <summary>
    /// 商品に関連するビジネスロジックを実装するサービスクラス
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        /// <summary>
        /// 非同期でIDを元に商品を取得します。
        /// </summary>
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            return product ?? throw new NullReferenceException($"Product with ID {productId} not found.");
        }

        /// <summary>
        /// 非同期でキーワードとカテゴリーに基づいて商品を検索します。
        /// </summary>
        public async Task<IEnumerable<Product>> SearchProductsAsync(string keyword, string category)
        {
            return await _productRepository.SearchProductsAsync(keyword, category);
        }

        /// <summary>
        /// 非同期で新しい商品を追加します。
        /// </summary>
        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddProductAsync(product);
        }

        /// <summary>
        /// 非同期で商品情報を更新します。
        /// </summary>
        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }
    }
}
