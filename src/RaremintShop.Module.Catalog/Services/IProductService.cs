using RaremintShop.Module.Catalog.Models;

namespace RaremintShop.Module.Catalog.Services
{
    /// <summary>
    /// 商品に関連するビジネスロジックを提供するためのインターフェース
    /// </summary>
    public interface IProductService
    {
        // 全商品取得
        Task<List<CatalogViewModel>> GetAllProductsAsync();

        // 全カテゴリ取得
        Task<List<Category>> GetAllCategoriesAsync();

        // 商品の登録
        Task<bool> RegisterCategoryAsync(Category category);

        // カテゴリ名のバリデーション
        Task<bool> CategoryNameExistsAsync(string categoryName);
    }
}
