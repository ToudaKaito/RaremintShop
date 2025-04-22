using RaremintShop.Module.Catalog.Models;
using RaremintShop.Module.Catalog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaremintShop.Module.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        // 全カテゴリ取得
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategoriesAsync();
                if (categories.Any())
                {
                    return categories.ToList();
                }
                else
                {
                    return new List<Category>();
                }
            }
            catch
            {
                throw;
            }
        }

        // カテゴリの登録
        public async Task<bool> RegisterCategoryAsync(Category category)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(category);
                if (CategoryNameExistsAsync(category.Name).Result)
                {
                    throw new InvalidOperationException("カテゴリ名が既に存在します。");
                }

                var result = await _categoryRepository.RegisterCategoryAsync(category);
                return result;
            }
            catch
            {
                throw;
            }
        }

        // カテゴリ名のバリデーション
        public async Task<bool> CategoryNameExistsAsync(string categoryName)
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategoriesAsync();
                return categories.Any(c => c.Name == categoryName);
            }
            catch
            {
                throw;
            }

        }
    }
}
