using RaremintShop.Module.Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaremintShop.Module.Catalog.Repositories
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// 全てのカテゴリを非同期に取得します。
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task<bool> RegisterCategoryAsync(Category category);
    }
}
