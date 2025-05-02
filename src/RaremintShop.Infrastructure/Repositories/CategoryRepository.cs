using Microsoft.EntityFrameworkCore;
using RaremintShop.Module.Catalog.Data;
using RaremintShop.Module.Catalog.Models;
using RaremintShop.Module.Catalog.Repositories;

namespace RaremintShop.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogDbContext context) : base(context)
        {
        }
    }
}
