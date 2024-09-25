using RaremintShop.Module.Orders.Models;
using RaremintShop.Module.Orders.Repositories;

namespace RaremintShop.Module.Orders.Services
{
    /// <summary>
    /// 注文詳細に関連するビジネスロジックを実装するサービスクラス
    /// </summary>
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        /// <summary>
        /// 非同期で注文IDと商品IDに基づいて注文詳細を取得します。
        /// </summary>
        /// <param name="orderId">注文のID</param>
        /// <param name="productId">商品のID</param>
        /// <returns>注文詳細エンティティ</returns>
        public async Task<OrderDetail> GetOrderDetailByIdAsync(int orderId, int productId)
        {
            return await _orderDetailRepository.GetOrderDetailByIdAsync(orderId, productId);
        }

        /// <summary>
        /// 非同期で注文IDに基づいて注文詳細を取得します。
        /// </summary>
        /// <param name="orderId">注文のID</param>
        /// <returns>注文詳細エンティティのリスト</returns>
        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            return await _orderDetailRepository.GetOrderDetailsByOrderIdAsync(orderId);
        }

        /// <summary>
        /// 非同期で新しい注文詳細を追加します。
        /// </summary>
        /// <param name="orderDetail">追加する注文詳細エンティティ</param>
        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            await _orderDetailRepository.AddOrderDetailAsync(orderDetail);
        }

        /// <summary>
        /// 非同期で既存の注文詳細を更新します。
        /// </summary>
        /// <param name="orderDetail">更新する注文詳細エンティティ</param>
        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            await _orderDetailRepository.UpdateOrderDetailAsync(orderDetail);
        }
    }
}
