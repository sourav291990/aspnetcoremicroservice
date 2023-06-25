using Discount.GRPC.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace Discount.GRPC.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DiscountDBContext _dbContext;

        public DiscountRepository(DiscountDBContext context)
        {
            _dbContext = context;
        }
        public async Task<Coupon> CreateCoupon(Coupon coupon)
        {
            await _dbContext.AddAsync(coupon);
            await _dbContext.SaveChangesAsync();

            return coupon;
        }

        public async Task DeleteCoupon(string productId)
        {
            var coupon = GetCoupon(productId);
            if (null == coupon)
            {
                throw new Exception($"No coupon found for product id {productId}.");
            }
            _dbContext.Remove(coupon);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Coupon> GetCoupon(string productId)
        {
            return await _dbContext?.Coupons?.FirstOrDefaultAsync(x => x.ProductId == productId);
        }
    }
}
