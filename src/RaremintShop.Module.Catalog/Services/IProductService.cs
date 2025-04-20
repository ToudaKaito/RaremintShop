using RaremintShop.Module.Catalog.Models;
using Microsoft.AspNetCore.Http;

namespace RaremintShop.Module.Catalog.Services
{
    /// <summary>
    /// 商品に関連するビジネスロジックを提供するためのインターフェース
    /// </summary>
    public interface IProductService
    {
        // 全商品取得
        Task<List<CatalogViewModel>> GetAllProductsAsync();





        // 商品の登録
        Task RegisterProductAsync(ProductRegisterViewModel model);
    }
}
