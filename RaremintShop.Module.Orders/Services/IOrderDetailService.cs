using RaremintShop.Module.Orders.Models;

namespace RaremintShop.Module.Orders.Services
{
    public interface IOrderDetailService
    {
        /// <summary>
        /// 注文IDと商品IDに基づいて注文詳細を非同期的に取得します。
        /// </summary>
        /// <param name="orderId">注文ID</param>
        /// <param name="productId">商品ID</param>
        /// <returns>注文詳細エンティティ</returns>
        Task<OrderDetail> GetOrderDetailByIdAsync(int orderId, int productId);

        /// <summary>
        /// 注文IDに基づいて注文詳細を非同期的に取得します。
        /// </summary>
        /// <param name="orderId">注文ID</param>
        /// <returns>注文詳細エンティティのリスト</returns>
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId);

        /// <summary>
        /// 新しい注文詳細を非同期的に追加します。
        /// </summary>
        /// <param name="orderDetail">追加する注文詳細エンティティ</param>
        Task AddOrderDetailAsync(OrderDetail orderDetail);

        /// <summary>
        /// 注文詳細を非同期的に更新します。
        /// </summary>
        /// <param name="orderDetail">更新する注文詳細エンティティ</param>
        Task UpdateOrderDetailAsync(OrderDetail orderDetail);
    }
}
