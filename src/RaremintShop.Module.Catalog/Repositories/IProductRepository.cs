using RaremintShop.Module.Catalog.Models;

namespace RaremintShop.Module.Catalog.Repositories
{
    /// <summary>
    /// 商品情報を管理するためのリポジトリインターフェース
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// 商品IDに基づいて商品を非同期的に取得します。
        /// </summary>
        /// <param name="productId">取得する商品のID</param>
        /// <returns>商品エンティティ</returns>
        Task<Product> GetProductByIdAsync(int productId);

        /// <summary>
        /// キーワードとカテゴリーに基づいて商品を非同期的に検索します。
        /// </summary>
        /// <param name="keyword">検索するキーワード</param>
        /// <param name="category">検索するカテゴリー</param>
        /// <returns>商品のリスト</returns>
        Task<IEnumerable<Product>> SearchProductsAsync(string keyword, string category);

        /// <summary>
        /// 新しい商品を非同期的に追加します。
        /// </summary>
        /// <param name="product">追加する商品エンティティ</param>
        Task AddProductAsync(Product product);

        /// <summary>
        /// 既存の商品情報を非同期的に更新します。
        /// </summary>
        /// <param name="product">更新する商品エンティティ</param>
        Task UpdateProductAsync(Product product);

        ///TODO
        ///商品詳細のリストを取得するメソッドを作成
    }
}
