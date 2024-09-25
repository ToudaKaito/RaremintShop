using RaremintShop.Module.Orders.Models;

namespace RaremintShop.Module.Orders.Repositories
{
    /// <summary>
    /// 注文情報を管理するためのリポジトリインターフェース
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// 注文IDに基づいて注文を非同期的に取得します。
        /// </summary>
        /// <param name="orderId">取得する注文のID</param>
        /// <returns>注文エンティティ</returns>
        Task<Order> GetOrderByIdAsync(int orderId);

        /// <summary>
        /// ユーザーIDに基づいて注文を非同期的に取得します。
        /// </summary>
        /// <param name="userId">取得するユーザーのID</param>
        /// <returns>注文エンティティのリスト</returns>
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);

        /// <summary>
        /// 新しい注文を非同期的に追加します。
        /// </summary>
        /// <param name="order">追加する注文エンティティ</param>
        Task AddOrderAsync(Order order);

        /// <summary>
        /// 既存の注文情報を非同期的に更新します。
        /// </summary>
        /// <param name="order">更新する注文エンティティ</param>
        Task UpdateOrderAsync(Order order);

        ///TODO
        ///外部キーのユーザーを取得するメソッドを作成
    }
}
