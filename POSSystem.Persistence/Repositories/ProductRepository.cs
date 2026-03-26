using POSSystem.Application.Interfaces.Repositories;
using POSSystem.Domain.Entities;
using POSSystem.Persistence.Context;

namespace POSSystem.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}