using System.Collections.Generic;
using RaremintShop.Models;

namespace RaremintShop.Repositories
{
    /// <summary>
    /// 商品情報を管理するためのリポジトリインターフェース
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// 商品IDに基づいて商品を取得します。
        /// </summary>
        /// <param name="productId">取得する商品のID</param>
        /// <returns>商品エンティティ</returns>
        Product GetProductById(int productId);

        /// <summary>
        /// キーワードとカテゴリーに基づいて商品を検索します。
        /// </summary>
        /// <param name="keyword">検索するキーワード</param>
        /// <param name="category">検索するカテゴリー</param>
        /// <returns>商品のリスト</returns>
        IEnumerable<Product> SearchProducts(string keyword, string category);

        /// <summary>
        /// 新しい商品を追加します。
        /// </summary>
        /// <param name="product">追加する商品エンティティ</param>
        void AddProduct(Product product);

        /// <summary>
        /// 既存の商品情報を更新します。
        /// </summary>
        /// <param name="product">更新する商品エンティティ</param>
        void UpdateProduct(Product product);
    }
}
