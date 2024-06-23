using MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Services;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceBasedFintechApp.OrderService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderCreateRequest orderCreateRequest)
        {
            return Ok(await _orderService.CreateOrder(orderCreateRequest));
        }
    }
}
