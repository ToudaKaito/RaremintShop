using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RaremintShop.Module.Orders.Data;
using RaremintShop.Module.Orders.Models;
using RaremintShop.Module.Orders.Repositories;

namespace RaremintShop.Infrastructure.Repositories
{
    /// <summary>
    /// 注文情報を管理するリポジトリの実装クラス
    /// </summary>
    public class OrderRepository : BaseRepository<OrdersDbContext>, IOrderRepository
    {
        /// <summary>
        /// コンストラクタ。データベースコンテキストとロガーを受け取ります。
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        /// <param name="logger">ロガーインスタンス</param>
        public OrderRepository(OrdersDbContext context, ILogger<OrderRepository> logger)
            : base(context, logger)
        {
        }

        /// <summary>
        /// 注文IDに基づいて注文を非同期的に取得します。
        /// </summary>
        /// <param name="orderId">取得する注文のID</param>
        /// <returns>注文エンティティ</returns>
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"注文ID {orderId} の注文が見つかりませんでした。");
            }
            return order;
        }

        /// <summary>
        /// ユーザーIDに基づいて注文を非同期的に取得します。
        /// </summary>
        /// <param name="userId">取得するユーザーのID</param>
        /// <returns>注文エンティティのリスト</returns>
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders.Where(o => o.UserID == userId).ToListAsync();
        }

        /// <summary>
        /// 新しい注文を非同期的に追加します。
        /// </summary>
        /// <param name="order">追加する注文エンティティ</param>
        public async Task AddOrderAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _context.Orders.Add(order);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex, "注文の追加", order.OrderID);
            }
        }

        /// <summary>
        /// 既存の注文情報を非同期的に更新します。
        /// </summary>
        /// <param name="order">更新する注文エンティティ</param>
        public async Task UpdateOrderAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _context.Orders.Update(order);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex, "注文の更新", order.OrderID);
            }
        }
    }
}
