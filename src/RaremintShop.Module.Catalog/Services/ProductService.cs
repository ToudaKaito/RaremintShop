using Microsoft.AspNetCore.Identity;
using RaremintShop.Module.Catalog.Models;
using RaremintShop.Module.Catalog.Repositories;
using RaremintShop.Shared.Services;
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
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new CatalogViewModel
            {
                Id = p.Id,
                Name = p.Name ?? string.Empty,
                Description = p.Description ?? string.Empty,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrls = p.Images.Select(i => i.ImagePath).ToList()
            }).ToList();
        }





        //// 商品の登録
        //public async Task<bool> RegisterProductAsync(ProductRegisterViewModel model)
        //{
        //    try
        //    {
        //        ArgumentNullException.ThrowIfNull(model);

        //        // Productのドメインモデルを作成
        //        var product = new Product
        //        {
        //            Name = model.Name,
        //            Price = model.Price,
        //            CategoryId = model.CategoryId,
        //            Description = model.Description,
        //            Stock = model.Stock,
        //            IsPublished = model.IsPublished,
        //            CreatedAt = DateTime.UtcNow,
        //            UpdatedAt = DateTime.UtcNow,
        //        };

        //        // 商品をDBに保存（リポジトリを通じて）
        //        var result = await _productRepository.RegisterProductAsync(product);

        //        if (!result)
        //        {
        //            return result;// 商品の保存に失敗した場合
        //        }

        //        // 画像が存在する場合のみ保存処理を行う
        //        if (model.Images != null && model.Images.Any())
        //        {
        //            // 現在登録されている画像の最大並び順を取得
        //            var currentMaxSortOrder = await _productRepository.GetMaxSortOrderAsync(product.Id);

        //            // 画像リストを作成
        //            var productImages = new List<ProductImage>();

        //            // 画像の並び順を設定しながら保存
        //            foreach (var (image, index) in model.Images.Select((value, index) => (value, index)))
        //            {
        //                // ファイル保存処理（サービスを通して）
        //                byte[] fileData;
        //                using (var memoryStream = new MemoryStream())
        //                {
        //                    await image.OpenReadStream().CopyToAsync(memoryStream);
        //                    fileData = memoryStream.ToArray(); // Stream を byte[] に変換
        //                }

        //                var imagePath = await _fileStorageService.SaveFileAsync(
        //                    fileData,        // ファイルのバイナリデータ
        //                    image.FileName,  // 元のファイル名
        //                    "products"       // 保存先のサブディレクトリ
        //                );

        //                // 保存後のファイルパスをProductImageとして追加
        //                productImages.Add(new ProductImage
        //                {
        //                    ProductId = product.Id, // 商品IDをセット（商品がDBに保存された後に設定される）
        //                    ImagePath = imagePath,
        //                    SortOrder = currentMaxSortOrder + index + 1, // 最大並び順 + 1 + インデックスで並べる
        //                });
        //            }

        //            // 画像リストをリポジトリ経由で保存
        //            var imageSaveResult = await _productRepository.RegisterProductImagesAsync(productImages);
        //            if (!imageSaveResult)
        //            {
        //                return false; // 画像保存に失敗した場合
        //            }
        //        }

        //        return true; // 商品と画像の保存が成功
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

    }
}
