using POSSystem.Application.Interfaces.Repositories;
using POSSystem.Domain.Entities;
using POSSystem.Persistence.Context;

namespace POSSystem.Persistence.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AppDbContext context) : base(context)
        {
        }
    }
}