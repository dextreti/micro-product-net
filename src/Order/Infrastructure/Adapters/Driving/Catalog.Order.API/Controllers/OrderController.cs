using Catalog.Order.Application.UseCases.CreatePurchaseOrders;
using Catalog.Order.Application.UseCases.GetPurchaseOrderById;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Order.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly CreatePurchaseOrderHandler _createHandler;
        private readonly GetPurchaseOrderByIdHandler _getHandler;

        public OrderController(CreatePurchaseOrderHandler createHandler, GetPurchaseOrderByIdHandler getHandler)
        {
            _createHandler = createHandler;
            _getHandler = getHandler;
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok("Order Service is running.");
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] GetPurchaseOrderByIdQuery request, CancellationToken ct)
        {
            var result = await _getHandler.ExecuteAsync(request, ct);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(new { message = result.Error });
        }

        
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreatePurchaseOrderCommand request, CancellationToken ct)
        {

            var result = await _createHandler.ExecuteAsync(request, ct);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

    }
}
