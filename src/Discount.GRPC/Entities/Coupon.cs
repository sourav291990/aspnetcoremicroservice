namespace Discount.GRPC.Entities
{
    public class Coupon
    {
        public string CouponId { get; set; }
        public string ProductId { get; set; }
        public double Amount { get; set; }
    }
}
