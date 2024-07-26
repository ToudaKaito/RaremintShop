using System;
using System.Collections.Generic;
using System.Linq;
using RaremintShop.Data;
using RaremintShop.Models;

namespace RaremintShop.Repositories
{
    /// <summary>
    /// 注文情報を管理するリポジトリの実装クラス
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// コンストラクタ。データベースコンテキストを受け取ります。
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// 注文IDに基づいて注文を取得します。
        /// </summary>
        /// <param name="orderId">取得する注文のID</param>
        /// <returns>注文エンティティ</returns>
        public Order GetOrderById(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if(order == null)
            {
                throw new NullReferenceException($"Order with ID {orderId} not found.");
            }
            return order;
        }

        /// <summary>
        /// ユーザーIDに基づいて注文を取得します。
        /// </summary>
        /// <param name="userId">取得するユーザーのID</param>
        /// <returns>注文エンティティのリスト</returns>
        public IEnumerable<Order> GetOrdersByUserId(int userId)
        {
            return _context.Orders.Where(o => o.UserID == userId).ToList();
        }

        /// <summary>
        /// 新しい注文を追加します。
        /// </summary>
        /// <param name="order">追加する注文エンティティ</param>
        public void AddOrder(Order order)
        {
            if(order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        /// <summary>
        /// 既存の注文情報を更新します。
        /// </summary>
        /// <param name="order">更新する注文エンティティ</param>
        public void UpdateOrder(Order order)
        {
            if(order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

    }
}
