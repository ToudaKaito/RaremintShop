using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RaremintShop.Module.Orders.Data;
using RaremintShop.Module.Orders.Models;
using RaremintShop.Module.Orders.Repositories;

namespace RaremintShop.Infrastructure.Repositories
{
    public class OrderDetailRepository : BaseRepository<OrdersDbContext>, IOrderDetailRepository
    {
        /// <summary>
        /// コンストラクタ。データベースコンテキストとロガーを受け取ります。
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        /// <param name="logger">ロガーインスタンス</param>
        public OrderDetailRepository(OrdersDbContext context, ILogger<OrderDetailRepository> logger)
            : base(context, logger)
        {
        }
        
        /// <summary>
        /// 注文IDと商品IDに基づいて注文詳細を取得します。
        /// </summary>
        /// <param name="orderId">注文のID</param>
        /// <param name="productId">商品のID</param>
        /// <returns>注文詳細エンティティ</returns>
        public async Task<OrderDetail> GetOrderDetailByIdAsync(int orderId, int productId)
        {
            // 複合キーを使用してエンティティを取得
            var orderDetail = await _context.OrderDetails
                .SingleOrDefaultAsync(od => od.OrderID == orderId && od.ProductID == productId);

            if (orderDetail == null)
            {
                throw new KeyNotFoundException($"注文ID {orderId} と商品ID {productId} に該当する注文詳細が見つかりませんでした。");
            }

            return orderDetail;
        }

        /// <summary>
        /// 注文IDに基づいて注文詳細を取得します。
        /// </summary>
        /// <param name="orderId">取得する注文のID</param>
        /// <returns>注文詳細エンティティのリスト</returns>
        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            return await _context.OrderDetails
                .Where(od => od.OrderID == orderId)
                .ToListAsync();
        }

        /// <summary>
        /// 新しい注文詳細を追加します。
        /// </summary>
        /// <param name="orderDetail">追加する注文詳細エンティティ</param>
        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            if (orderDetail == null)
                throw new ArgumentNullException(nameof(orderDetail));

            _context.OrderDetails.Add(orderDetail);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex, "注文詳細の追加", orderDetail.OrderID);
            }
        }

        /// <summary>
        /// 既存の注文詳細を更新します。
        /// </summary>
        /// <param name="orderDetail">更新する注文詳細エンティティ</param>
        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            if (orderDetail == null)
                throw new ArgumentNullException(nameof(orderDetail));

            _context.OrderDetails.Update(orderDetail);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex, "注文詳細の更新", orderDetail.OrderID);
            }
        }
    }
}
