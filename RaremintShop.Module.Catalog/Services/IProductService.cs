using RaremintShop.Module.Catalog.Models;

namespace RaremintShop.Module.Catalog.Services
{
    /// <summary>
    /// 商品に関連するビジネスロジックを提供するためのインターフェース
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// IDを元に商品を非同期的に取得します。
        /// </summary>
        /// <param name="productId">取得する商品のID</param>
        /// <returns>指定されたIDの商品</returns>
        Task<Product> GetProductByIdAsync(int productId);

        /// <summary>
        /// キーワードとカテゴリーに基づいて商品を非同期的に検索します。
        /// </summary>
        /// <param name="keyword">検索するキーワード</param>
        /// <param name="category">検索するカテゴリー</param>
        /// <returns>該当する商品一覧</returns>
        Task<IEnumerable<Product>> SearchProductsAsync(string keyword, string category);

        /// <summary>
        /// 新しい商品を非同期的に追加します。
        /// </summary>
        /// <param name="product">追加する商品</param>
        Task AddProductAsync(Product product);

        /// <summary>
        /// 商品情報を非同期的に更新します。
        /// </summary>
        /// <param name="product">更新する商品</param>
        Task UpdateProductAsync(Product product);
    }
}
