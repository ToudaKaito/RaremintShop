using System.Collections.Generic;
using RaremintShop.Models;

namespace RaremintShop.Repositories
{
    /// <summary>
    /// 注文詳細情報を管理するためのリポジトリインターフェース
    /// </summary>
    public interface IOrderDetailRepository
    {
        /// <summary>
        /// 注文詳細IDに基づいて注文詳細を取得します。
        /// </summary>
        /// <param name="orderDetailId">取得する注文詳細のID</param>
        /// <returns>注文詳細エンティティ</returns>
        OrderDetail GetOrderDetailById(int orderDetailId);

        /// <summary>
        /// 注文IDに基づいて注文詳細を取得します。
        /// </summary>
        /// <param name="orderId">取得する注文のID</param>
        /// <returns>注文詳細エンティティのリスト</returns>
        IEnumerable<OrderDetail> GetOrderDetailsByOrderId(int orderId);

        /// <summary>
        /// 新しい注文詳細を追加します。
        /// </summary>
        /// <param name="orderDetail">追加する注文詳細エンティティ</param>
        void AddOrderDetail(OrderDetail orderDetail);

        /// <summary>
        /// 既存の注文詳細を更新します。
        /// </summary>
        /// <param name="orderDetail">更新する注文詳細エンティティ</param>
        void UpdateOrderDetail(OrderDetail orderDetail);
    }
}
