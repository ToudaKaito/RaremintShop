using RaremintShop.Models;
using System.Collections.Generic;

namespace RaremintShop.Repositories
{
    /// <summary>
    /// 注文情報を管理するためのリポジトリインターフェース
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// 注文IDに基づいて注文を取得します。
        /// </summary>
        /// <param name="orderId">取得する注文のID</param>
        /// <returns>注文エンティティ</returns>
        Order GetOrderById(int orderId);

        /// <summary>
        /// ユーザーIDに基づいて注文を取得します。
        /// </summary>
        /// <param name="userId">取得するユーザーのID</param>
        /// <returns>注文エンティティのリスト</returns>
        IEnumerable<Order> GetOrdersByUserId(int userId);

        /// <summary>
        /// 新しい注文を追加します。
        /// </summary>
        /// <param name="order">追加する注文エンティティ</param>
        void AddOrder(Order order);

        /// <summary>
        /// 既存の注文情報を更新します。
        /// </summary>
        /// <param name="order">更新する注文エンティティ</param>
        void UpdateOrder(Order order);
    }
}
