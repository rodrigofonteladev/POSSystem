using Microsoft.EntityFrameworkCore.Storage;
using POSSystem.Application.Interfaces.Repositories;

namespace POSSystem.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ISaleRepository SaleRepository { get; }
        public ISaleDetailRepository SaleDetailRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        public ICashBoxRepository CashBoxRepository { get; }

        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}