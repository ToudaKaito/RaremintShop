using RaremintShop.Module.Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaremintShop.Module.Catalog.Services
{
    public interface ICategoryService
    {
        // 全カテゴリ取得
        Task<List<Category>> GetAllCategoriesAsync();

        // カテゴリの登録
        Task<bool> RegisterCategoryAsync(Category category);

        // カテゴリ名のバリデーション
        Task<bool> CategoryNameExistsAsync(string categoryName);
    }
}
