using Logic.DTOs;

namespace Logic.Models
{
    public class Order(OrderDto orderDto)
    {
        public string Id { get; set; } = orderDto.Id;

        public string ClientName { get; set; } = orderDto.ClientName;

        public List<OrderItem> Itens { get; set; } = orderDto.Itens;

        public DateTime DtCreated { get; set; } = DateTime.UtcNow;
    }
}
