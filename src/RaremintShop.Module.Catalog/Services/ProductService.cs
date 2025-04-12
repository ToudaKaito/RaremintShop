using Microsoft.AspNetCore.Identity;
using RaremintShop.Module.Catalog.Models;
using RaremintShop.Module.Catalog.Repositories;
using System.Threading.Tasks;

namespace RaremintShop.Module.Catalog.Services
{
    /// <summary>
    /// 商品に関連するビジネスロジックを実装するサービスクラス
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        
        // 全商品取得
        public async Task<List<CatalogViewModel>> GetAllProductsAsync()
        {
            try
            {
                var products = await _productRepository.GetAllProductsAsync();
                var catalogList = new List<CatalogViewModel>();
                if (products.Any())
                {
                    return products.Select(p => new CatalogViewModel
                    {
                        Id = p.Id,
                        Name = p.Name ?? string.Empty, // Null 参照代入の可能性を回避
                        Description = p.Description ?? string.Empty, // Null 参照代入の可能性を回避
                        Price = p.Price,
                        StockQuantity = p.Stock, // Stock プロパティを StockQuantity にマッピング
                        ImageUrls = p.Images.Select(i => i.ImageUrl).ToList()
                    }).ToList();
                }
                else
                {
                    return catalogList;
                }
                
            }
            catch
            {
                throw;
            }
        }

        // 全カテゴリ取得
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _productRepository.GetAllCategoriesAsync();
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

        // 商品の登録
        public async Task<bool> RegisterCategoryAsync(Category category)
        {
            try
            {
                if (category == null)
                {
                    throw new ArgumentNullException(nameof(category));
                }
                if(CategoryNameExistsAsync(category.Name).Result)
                {
                    throw new InvalidOperationException("カテゴリ名が既に存在します。");
                }

                var result = await _productRepository.RegisterCategoryAsync(category);
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
                var categories = await _productRepository.GetAllCategoriesAsync();
                return categories.Any(c => c.Name == categoryName);
            }
            catch
            {
                throw;
            }

        }
    }
}
