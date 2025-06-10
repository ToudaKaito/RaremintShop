using RaremintShop.Core.Models;

namespace RaremintShop.Core.Interfaces.Repositories
{
    /// <summary>
    /// 商品情報を管理するためのリポジトリインターフェース
    /// </summary>
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<bool> ExistsByNameAsync(string name);



    }
}