using RaremintShop.Core.DTOs;

namespace RaremintShop.Core.Interfaces.Services
{
    /// <summary>
    /// 商品に関連するビジネスロジックを提供するためのインターフェース
    /// </summary>
    public interface IProductService
    {
        // 全商品取得
        Task<List<ProductDto>> GetAllProductsAsync();

        // 商品の登録
        Task RegisterProductAsync(ProductDto productDto, List<ProductImageData> imageDatas);

    }
}
