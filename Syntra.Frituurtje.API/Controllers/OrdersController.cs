using Microsoft.AspNetCore.Mvc;
using Syntra.Frituurtje.API.Services;
using Syntra.Frituurtje.Contracts.Models;

namespace Syntra.Frituurtje.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        IOrderManagerService OrderService { get; init; } = default!;
        public OrdersController(IOrderManagerService service) { 
            OrderService = service; 
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodOrder>>> GetOrders()
        {
            return Ok(await OrderService.GetAllAsync());
        }
        [HttpGet("find/client/{client}")]
        public async Task<ActionResult<FoodOrder>> FindOrder(string client)
        {
          
            return Ok(await OrderService.FindClientAsync(client));
        }
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<FoodOrder>>> FilterOrders(
            [FromQuery] string? id = null,
            [FromQuery] decimal? price = null, 
            [FromQuery] string? client = null, 
            [FromQuery] string? item = null,
            [FromQuery] DateOnly? orderDate = null)
        {
            return Ok(await OrderService.FilterAsync(id,price,client,item,orderDate));
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateOrder(FoodOrder order)
        {
            return await OrderService.CreateAsync(order);    
        }
        [HttpPut("manage/upsert")]
        public async Task<ActionResult<bool>> UpsertOrder(FoodOrder order)
        {
            return await OrderService.UpsertAsync(order);
        }
        [HttpDelete]
        public ActionResult DeleteOrder(string id)
        {
            return Ok(OrderService.DeleteAsync(id));    
        }

    }
}
