using RaremintShop.Module.Catalog.Models;
using RaremintShop.Module.Core.Interface;

namespace RaremintShop.Module.Catalog.Services
{
    public interface ICategoryService
    {
        // 全カテゴリ取得
        Task<List<Category>> GetAllCategoriesAsync();

        // カテゴリの登録
        Task<bool> RegisterCategoryAsync(Category category);

        // カテゴリ名のバリデーション
        Task<bool> CategoryNameExistsAsync(string categoryName, int? excludeId = null);

        // IDでカテゴリ取得
        Task<Category> GetCategoryByIdAsync(int id);

        // カテゴリの更新
        Task<bool> UpdateCategoryAsync(Category category);

        // カテゴリの削除
        Task<bool> DeleteCategoryAsync(int id);
    }
}
