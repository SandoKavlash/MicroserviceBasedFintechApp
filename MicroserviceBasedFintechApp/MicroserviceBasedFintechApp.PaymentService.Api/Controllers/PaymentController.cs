using FluentValidation;
using MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Services;
using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceBasedFintechApp.PaymentService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("Pay")]
        public async Task<IActionResult> Pay([FromBody] PaymentModel paymentModel, [FromServices] IValidator<PaymentModel> paymentValidator)
        {
            var validationResult = paymentValidator.Validate(paymentModel);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            PaymentResponse response = await _paymentService.Pay(paymentModel);
            if (!response.PayementDone) return BadRequest(response);
            
            return Ok(response);   
        }
    }
}
