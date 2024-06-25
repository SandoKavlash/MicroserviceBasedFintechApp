using MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Services;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
//TODO: web hook based compute orders

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

        [HttpGet]
        public IActionResult GetOrder([FromQuery] int orderId, [FromHeader] Guid apiKey, [FromHeader] Guid secret)
        {
            GetOrderRequest request = new GetOrderRequest()
            {
                ApiKey = apiKey,
                OrderId = orderId,
                Secret = secret
            };
            return Ok(_orderService.GetOrder(request));
        }
    }
}
