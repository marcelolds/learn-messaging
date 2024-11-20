using Logic.Models;

namespace Logic.DTOs
{
    public class OrderDto
    {
        public required string Id { get; set; }

        public required string ClientName { get; set; }

        public required List<OrderItem> Itens { get; set; }
    }
}
