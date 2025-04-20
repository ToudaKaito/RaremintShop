using RaremintShop.Module.Catalog.Models;

namespace RaremintShop.Module.Catalog.Repositories
{
    /// <summary>
    /// 商品情報を管理するためのリポジトリインターフェース
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// 全ての商品を非同期的に取得します。
        /// </summary>
        /// <returns>商品のリスト</returns>
        Task<IEnumerable<Product>> GetAllProductsAsync();

        

        Task<bool> RegisterProductAsync(Product product);
        Task<bool> RegisterProductImagesAsync(List<ProductImage> images);
    }
}
