using POSSystem.Application.Interfaces.Repositories;
using POSSystem.Domain.Entities;
using POSSystem.Persistence.Context;

namespace POSSystem.Persistence.Repositories
{
    public class CashBoxRepository : Repository<CashBox>, ICashBoxRepository
    {
        public CashBoxRepository(AppDbContext context) : base(context)
        {
        }
    }
}