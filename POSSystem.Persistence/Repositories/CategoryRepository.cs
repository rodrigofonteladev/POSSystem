using POSSystem.Application.Interfaces.Repositories;
using POSSystem.Domain.Entities;
using POSSystem.Persistence.Context;

namespace POSSystem.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}