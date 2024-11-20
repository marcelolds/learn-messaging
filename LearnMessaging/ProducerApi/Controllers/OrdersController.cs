using Logic.DTOs;
using Logic.Models;
using Logic.RabbitMq;
using Microsoft.AspNetCore.Mvc;

namespace ProducerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(RabbitMqClient mqClient) : ControllerBase
    {
        private readonly RabbitMqClient _mqClient = mqClient ?? throw new ArgumentNullException(nameof(mqClient));

        [HttpPost(Name = "ReceiveOrders")]
        public IActionResult PostAsync([FromBody] OrderDto orderDto)
        {
            try
            {
                if (orderDto == null)
                    return NoContent();

                var order = new Order(orderDto);
                var result = _mqClient.PublishQueue(order);
                return result.IsCompletedSuccessfully
                    ? Accepted()
                    : Problem("Failed to publish order");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
