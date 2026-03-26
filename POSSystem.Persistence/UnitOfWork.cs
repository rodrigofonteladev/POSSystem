using Microsoft.EntityFrameworkCore.Storage;
using POSSystem.Application.Interfaces;
using POSSystem.Application.Interfaces.Repositories;
using POSSystem.Persistence.Context;
using POSSystem.Persistence.Repositories;

namespace POSSystem.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        private IProductRepository? _productRepository;
        private ICategoryRepository? _categoryRepository;
        private ISaleRepository? _saleRepository;
        private ISaleDetailRepository? _saleDetailRepository;
        private IRefreshTokenRepository? _refreshTokenRepository;
        private ICashBoxRepository? _cashBoxRepository;

        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);
        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);
        public ISaleRepository SaleRepository => _saleRepository ??= new SaleRepository(_context);
        public ISaleDetailRepository SaleDetailRepository => _saleDetailRepository ??= new SaleDetailRepository(_context);
        public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository ??= new RefreshTokenRepository(_context);
        public ICashBoxRepository CashBoxRepository => _cashBoxRepository ??= new CashBoxRepository(_context);

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}