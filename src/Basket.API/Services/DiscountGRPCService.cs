
using Discount.GRPC.Protos;

namespace Basket.API.Services
{
    public class DiscountGRPCService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoClient;

        public DiscountGRPCService(DiscountProtoService.DiscountProtoServiceClient discountProtoClient)
        {
            _discountProtoClient = discountProtoClient;
        }

        public async Task<CouponModel> GetDiscount(string productId)
        {
            var discountRequest = new GetDiscountRequest { ProductId = productId };
            return await _discountProtoClient.GetDiscountAsync(discountRequest);
        }

        public async Task<CouponModel> CreateDiscount(CouponModel coupon)
        {
            var createDiscoutRequest = new CreateDiscoutRequest { Coupon = coupon };
            return await _discountProtoClient.CreateDiscountAsync(createDiscoutRequest);
        }

        public async Task<bool> DeleteDiscount(string productId)
        {
            var deleteDiscoutRequest = new DeleteDiscoutRequest { ProductId = productId };
            var deleteResult = await _discountProtoClient.DeleteDiscountAsync(deleteDiscoutRequest);
            return deleteResult.Success;
        }
    }
}
