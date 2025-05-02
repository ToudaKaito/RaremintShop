using RaremintShop.Module.Catalog.Models;
using RaremintShop.Module.Core.Interface;

namespace RaremintShop.Module.Catalog.Repositories
{
    /// <summary>
    /// 商品情報を管理するためのリポジトリインターフェース
    /// </summary>
    public interface IProductRepository : IBaseRepository<Product>
    {
        /// <summary>
        /// 商品画像を登録します。
        /// </summary>
        /// <param name="images">登録する画像のリスト</param>
        /// <returns>登録成功の場合は true</returns>
        Task<bool> RegisterProductImagesAsync(List<ProductImage> images);

        /// <summary>
        /// 指定された商品に関連付けられた画像の最大並び順を取得します。
        /// </summary>
        /// <param name="productId">商品ID</param>
        /// <returns>最大並び順（画像が存在しない場合は 0）</returns>
        Task<int> GetMaxSortOrderAsync(int productId);
    }
}