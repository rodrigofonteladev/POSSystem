using Mapster;
using Microsoft.AspNetCore.Mvc;
using POSSystem.Application.DTOs.CashBoxes;
using POSSystem.Application.Interfaces;
using POSSystem.Domain.Entities;
using POSSystem.Infrastructure.Extensions;

namespace POSSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CashBoxController : ControllerBase
    {
        private readonly ICashBoxService _cashBoxService;
        private readonly IUnitOfWork _unitOfWork;

        public CashBoxController(ICashBoxService cashBoxService, IUnitOfWork unitOfWork)
        {
            _cashBoxService = cashBoxService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("open")]
        public async Task<IActionResult> Open(OpenCashBoxDto dto)
        {
            var userId = User.GetUserId();

            var result = await _cashBoxService.OpenAsync(dto, userId);
            return Ok(new { message = result.Message });
        }

        [HttpPost("close")]
        public async Task<IActionResult> Close(CloseCashBoxDto dto)
        {
            var userId = User.GetUserId();

            var result = await _cashBoxService.CloseAsync(dto, userId);
            if (!result.IsSuccess) return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }
    }
}