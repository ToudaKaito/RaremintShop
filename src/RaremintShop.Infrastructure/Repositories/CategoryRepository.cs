using RaremintShop.Core.Interfaces.Repositories;
using RaremintShop.Core.Models;
using RaremintShop.Infrastructure.Data;

namespace RaremintShop.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
