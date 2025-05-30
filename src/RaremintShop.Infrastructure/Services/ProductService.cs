using RaremintShop.Core.DTOs;
using RaremintShop.Core.Interfaces.Repositories;
using RaremintShop.Core.Interfaces.Services;
using RaremintShop.Core.Models;
using RaremintShop.Shared.Exceptions;
using RaremintShop.Shared.Services;
using static RaremintShop.Shared.Constants;

namespace RaremintShop.Infrastructure.Services
{
    /// <summary>
    /// 商品に関連するビジネスロジックを実装するサービスクラス
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IFileStorageService _fileStorageService;

        public ProductService(IProductRepository productRepository, IProductImageRepository productImageRepository, IFileStorageService fileStorageService)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productImageRepository = productImageRepository ?? throw new ArgumentNullException(nameof(productImageRepository));
            _fileStorageService = fileStorageService ?? throw new ArgumentNullException(nameof(fileStorageService));
        }


        // 全商品取得
        public async Task<List<ProductDto>> GetAllProductsAsync()
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
        public async Task RegisterProductAsync(ProductDto productDto, List<ProductImageData> imageDatas)
        {
            ArgumentNullException.ThrowIfNull(productDto, nameof(productDto)); // nullの場合は例外をスロー
            ArgumentNullException.ThrowIfNull(imageDatas, nameof(imageDatas)); // nullの場合は例外をスロー

            // 重複チェック
            if(await _productRepository.ExistsByNameAsync(productDto.Name))
            {
                throw new BusinessException(ErrorMessages.DuplicateName);
            }

            // Entityへ変換
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                Description = productDto.Description,
                Stock = productDto.Stock,
                IsPublished = productDto.IsPublished,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            // 商品をDBに保存
            var result = await _productRepository.AddAsync(product);
            if (!result)
            {
                throw new BusinessException(ErrorMessages.CategoryRegisterError);
            }

            // 画像が存在する場合のみ保存処理
            if (imageDatas.Any())
            {
                // 現在登録されている画像の最大並び順を取得
                var currentMaxSortOrder = await _productImageRepository.GetMaxSortOrderAsync(product.Id);

                // 画像リストを作成
                var productImages = new List<ProductImage>();

                // 画像の並び順を設定しながら保存
                foreach (var (imageData, index) in imageDatas.Select((value, index) => (value, index)))
                {
                    // ファイル保存処理
                    var imagePath = await _fileStorageService.SaveFileAsync(
                        imageData.Data,        // ファイルのバイナリデータ
                        imageData.FileName,    // 元のファイル名
                        "products"             // 保存先のサブディレクトリ
                    );

                    // 保存後のファイルパスをProductImageとして追加
                    productImages.Add(new ProductImage
                    {
                        ProductId = product.Id, // 商品IDをセット（商品がDBに保存された後に設定される）
                        ImagePath = imagePath,
                        SortOrder = currentMaxSortOrder + index + 1, // 最大並び順 + 1 + インデックスで並べる
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    });
                }

                // 画像リストをリポジトリ経由で保存
                var imageSaveResult = await _productImageRepository.AddRangeAsync(productImages);
                if (!imageSaveResult)
                {
                    throw new BusinessException(ErrorMessages.ProductImagesRegisterError);
                }
            }
        }

    }
}
