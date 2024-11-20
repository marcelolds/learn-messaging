namespace Logic.Models
{
    public class OrderItem(string name, int quantity, decimal price)
    {
        public string Name { get; set; } = name;
        public int Quantity { get; set; } = quantity;
        public decimal Price { get; set; } = price;
    }
}
