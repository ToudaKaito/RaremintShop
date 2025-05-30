using RaremintShop.Core.Models;
namespace RaremintShop.Core.Interfaces.Repositories
{
    public interface IProductImageRepository : IBaseRepository<ProductImage>
    {
        Task<int> GetMaxSortOrderAsync(int productId);
    }
}
