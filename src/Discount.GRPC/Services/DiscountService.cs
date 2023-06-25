
using Discount.GRPC.Entities;
using Discount.GRPC.Protos;
using Discount.GRPC.Repositories;
using Grpc.Core;

namespace Discount.GRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger)
        {
            _discountRepository = discountRepository;
            _logger = logger;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetCoupon(request.ProductId);
            if (null == coupon)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No product present with product id {request.ProductId}"));
            }
            return new CouponModel { CouponId = coupon.CouponId, ProductId = coupon.ProductId, Amount = coupon.Amount };
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscoutRequest request, ServerCallContext context)
        {
            var coupon = new Coupon { CouponId = request.Coupon.CouponId, ProductId = request.Coupon.ProductId, Amount = request.Coupon.Amount };
            try
            {
                await _discountRepository.CreateCoupon(coupon);
            }
            catch (Exception)
            {
                throw;
            }
            return request.Coupon;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscoutRequest request, ServerCallContext context)
        {
            try
            {
                await _discountRepository.DeleteCoupon(request.ProductId);
            }
            catch (Exception ex)
            {
                return new DeleteDiscountResponse { Success = false };
            }
            return new DeleteDiscountResponse { Success = true };
        }
    }
}
