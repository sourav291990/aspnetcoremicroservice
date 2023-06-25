using Discount.GRPC.Entities;

namespace Discount.GRPC.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetCoupon(string productId);
        Task<Coupon> CreateCoupon(Coupon coupon);
        Task DeleteCoupon(string productId);
    }
}
