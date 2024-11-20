using Logic.DTOs;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProducerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        [HttpPost(Name = "ReceiveOrders")]
        public IActionResult Post([FromBody] OrderDto orderDto)
        {
            try
            {
                var order = new Order(orderDto);
                //salvar no rabbitmq

                return Accepted();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
