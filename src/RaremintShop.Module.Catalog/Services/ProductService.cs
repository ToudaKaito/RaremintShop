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
        private readonly IFileStorageService _fileStorageService;

        public ProductService(IProductRepository productRepository, IFileStorageService fileStorageService)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _fileStorageService = fileStorageService ?? throw new ArgumentNullException(nameof(fileStorageService));
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





        // 商品の登録
        public async Task RegisterProductAsync(ProductRegisterViewModel model)
        {
            // Productのドメインモデルを作成
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Stock = model.Stock,
                IsPublished = model.IsPublished,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Images = new List<ProductImage>()
            };

            // 画像が存在する場合のみ保存処理を行う
            if (model.Images != null && model.Images.Any())
            {
                foreach (var image in model.Images)
                {
                    // ファイル保存処理（サービスを通して）
                    var imagePath = await _fileStorageService.SaveFileAsync(image);

                    // 保存後のファイルパスをProductImageとして追加
                    product.Images.Add(new ProductImage
                    {
                        ImageUrl = imagePath
                    });
                }
            }

            // 商品をDBに保存（リポジトリを通じて）
            await _productRepository.AddAsync(product);
        }

    }
}
