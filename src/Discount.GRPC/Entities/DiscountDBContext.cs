using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Entities
{
    public class DiscountDBContext: DbContext
    {

        public DiscountDBContext(DbContextOptions<DiscountDBContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
