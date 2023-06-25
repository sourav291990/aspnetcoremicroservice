namespace Basket.API.Entities
{
    public class ShoppingCartItem
    {
        public string ProductName { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string ProductId { get; set; }
    }
}
