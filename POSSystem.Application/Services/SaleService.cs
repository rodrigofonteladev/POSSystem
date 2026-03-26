using POSSystem.Application.DTOs.Sales;
using POSSystem.Application.Interfaces;
using POSSystem.Domain.Entities;
using POSSystem.Domain.Enums;

namespace POSSystem.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateSaleResponseDto> CreateSaleAsync(CreateSaleDto saleDto, string userId)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var activeSession = await _unitOfWork.CashBoxRepository
                    .GetAsync(cb => cb.UserId == userId && cb.Status == CashBoxStatus.Open);

                if (activeSession == null) return new CreateSaleResponseDto
                {
                    Message = "The user doesn't have an open checkout"
                };

                var sale = new Sale
                {
                    UserId = userId,
                    CashBoxId = activeSession.Id,
                    SaleDetails = new List<SaleDetail>()
                };

                decimal calculatedTotal = 0m;

                foreach (var item in saleDto.Details)
                {
                    var product = await _unitOfWork.ProductRepository
                        .GetByIdAsync(item.ProductId);
                    if (product == null)
                        throw new Exception($"Product {item.ProductId} not found");
                    if (!product.IsActive)
                        throw new Exception($"Product {item.ProductId} not available");
                    if (product.Stock < item.Quantity)
                        throw new Exception($"Insufficient stock for {product.Name}");

                    product.Stock -= item.Quantity;
                    _unitOfWork.ProductRepository.Update(product);

                    var subTotal = item.Quantity * product.Price;
                    calculatedTotal += subTotal;

                    sale.SaleDetails.Add(new SaleDetail
                    {
                        Quantity = item.Quantity,
                        UnitPrice = product.Price,
                        ProductId = item.ProductId
                    });
                }

                sale.Total = calculatedTotal;
                activeSession.TotalSales = (activeSession.TotalSales ?? 0) + calculatedTotal;

                _unitOfWork.SaleRepository.Create(sale);

                await _unitOfWork.CompleteAsync();
                await transaction.CommitAsync();

                return new CreateSaleResponseDto
                {
                    IsSuccess = true,
                    Message = "Sale created successfully"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new CreateSaleResponseDto
                {
                    Message = $"Error: {ex.Message}"
                };
            }
        }
    }
}