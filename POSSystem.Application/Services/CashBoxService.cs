using Mapster;
using POSSystem.Application.DTOs.CashBoxes;
using POSSystem.Application.Interfaces;
using POSSystem.Domain.Entities;
using POSSystem.Domain.Enums;

namespace POSSystem.Application.Services
{
    public class CashBoxService : ICashBoxService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CashBoxService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CashBoxResponseDto> OpenAsync(OpenCashBoxDto dto, string userId)
        {
            var hasOpenBox = await _unitOfWork.CashBoxRepository
                .GetAsync(cb => cb.UserId == userId && cb.Status == CashBoxStatus.Open);
            if (hasOpenBox != null) return new CashBoxResponseDto
            {
                Message = $"User already has an open cash box."
            };

            var cashBox = dto.Adapt<CashBox>();
            cashBox.UserId = userId;
            cashBox.StartTime = DateTime.UtcNow;

            _unitOfWork.CashBoxRepository.Create(cashBox);

            await _unitOfWork.CompleteAsync();
            return new CashBoxResponseDto
            {
                IsSuccess = true,
                Message = "Cash box opened successfully"
            };
        }

        public async Task<CashBoxResponseDto> CloseAsync(CloseCashBoxDto dto, string userId)
        {
            var cashBox = await _unitOfWork.CashBoxRepository
                .GetAsync(cb =>
                    cb.Id == dto.Id &&
                    cb.UserId == userId &&
                    cb.Status == CashBoxStatus.Open
                );
            if (cashBox == null) return new CashBoxResponseDto
            {
                Message = "Error closing cash register"
            };

            cashBox.FinalAmount = dto.FinalAmount;
            cashBox.EndTime = DateTime.UtcNow;

            decimal expectedAmount = cashBox.InitialAmount + (cashBox.TotalSales ?? 0);
            cashBox.Difference = cashBox.FinalAmount - expectedAmount;
            cashBox.Status = CashBoxStatus.Closed;

            await _unitOfWork.CompleteAsync();

            return new CashBoxResponseDto
            {
                IsSuccess = true,
                Message = $"Box closed. Difference: {cashBox.Difference:C2}"
            };
        }
    }
}