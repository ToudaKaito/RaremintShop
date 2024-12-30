using RaremintShop.Module.Orders.Models;
using RaremintShop.Module.Orders.Repositories;

namespace RaremintShop.Module.Orders.Services
{
    /// <summary>
    /// 注文に関連するビジネスロジックを実装するサービスクラス
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// 非同期で注文IDに基づいて注文を取得します。
        /// </summary>
        /// <param name="orderId">注文のID</param>
        /// <returns>注文エンティティ</returns>
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        /// <summary>
        /// 非同期でユーザーIDに基づいてユーザーの注文を取得します。
        /// </summary>
        /// <param name="userId">ユーザーのID</param>
        /// <returns>注文エンティティのリスト</returns>
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _orderRepository.GetOrdersByUserIdAsync(userId);
        }

        /// <summary>
        /// 非同期で新しい注文を追加します。
        /// </summary>
        /// <param name="order">追加する注文エンティティ</param>
        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
        }

        /// <summary>
        /// 非同期で既存の注文を更新します。
        /// </summary>
        /// <param name="order">更新する注文エンティティ</param>
        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateOrderAsync(order);
        }
    }
}
