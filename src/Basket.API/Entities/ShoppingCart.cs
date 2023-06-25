namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public ICollection<ShoppingCartItem> Items { get; set; }
        public double TotalPrice
        {
            get
            {
                double totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
        public ShoppingCart()
        {
        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
    }
}
