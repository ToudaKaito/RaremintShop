using System;
using System.Collections.Generic;
using System.Linq;
using RaremintShop.Data;
using RaremintShop.Models;

namespace RaremintShop.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// コンストラクタ。データベースコンテキストを受け取ります。
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        public OrderDetailRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// 注文IDと商品IDに基づいて注文詳細を取得します。
        /// </summary>
        /// <param name="orderId">注文のID</param>
        /// <param name="productId">商品のID</param>
        /// <returns>注文詳細エンティティ</returns>
        public OrderDetail GetOrderDetailById(int orderId, int productId)
        {
            // 複合キーを使用してエンティティを取得
            var orderDetail = _context.OrderDetails
                .SingleOrDefault(od => od.OrderID == orderId && od.ProductID == productId);

            if (orderDetail == null)
            {
                throw new NullReferenceException($"OrderDetail with OrderID {orderId} and ProductID {productId} not found.");
            }

            return orderDetail;
        }

        /// <summary>
        /// 注文IDに基づいて注文詳細を取得します。
        /// </summary>
        /// <param name="orderId">取得する注文のID</param>
        /// <returns>注文詳細エンティティのリスト</returns>
        public IEnumerable<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            return _context.OrderDetails.Where(od => od.OrderID == orderId).ToList();
        }

        /// <summary>
        /// 新しい注文詳細を追加します。
        /// </summary>
        /// <param name="orderDetail">追加する注文詳細エンティティ</param>
        public void AddOrderDetail(OrderDetail orderDetail)
        {
            if(orderDetail == null)
            {
                throw new ArgumentException(nameof(orderDetail));
            }
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }

        /// <summary>
        /// 既存の注文詳細を更新します。
        /// </summary>
        /// <param name="orderDetail">更新する注文詳細エンティティ</param>
        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            if(orderDetail == null)
            {
                throw new ArgumentException(nameof (orderDetail));
            }
            _context.OrderDetails.Update(orderDetail);
            _context.SaveChanges();
        }
    }
}
