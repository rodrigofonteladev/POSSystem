using POSSystem.Application.Interfaces.Repositories;
using POSSystem.Domain.Entities;
using POSSystem.Persistence.Context;

namespace POSSystem.Persistence.Repositories
{
    public class SaleDetailRepository : Repository<SaleDetail>, ISaleDetailRepository
    {
        public SaleDetailRepository(AppDbContext context) : base(context)
        {
        }
    }
}