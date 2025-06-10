using RaremintShop.Core.DTOs;
using RaremintShop.Core.Models;

namespace RaremintShop.Core.Interfaces.Services
{
    public interface ICategoryService
    {
        // 全カテゴリ取得
        Task<List<CategoryDto>> GetAllCategoriesAsync();

        // カテゴリの登録
        Task RegisterCategoryAsync(CategoryDto categoryDto);

        // カテゴリ名のバリデーション
        Task<bool> CategoryNameExistsAsync(string categoryName, int? excludeId = null);

        // IDでカテゴリ取得
        Task<CategoryDto> GetCategoryByIdAsync(int id);

        // カテゴリの更新
        Task UpdateCategoryAsync(CategoryDto categoryDto);

        // カテゴリの削除
        Task DeleteCategoryAsync(int id);
    }
}
