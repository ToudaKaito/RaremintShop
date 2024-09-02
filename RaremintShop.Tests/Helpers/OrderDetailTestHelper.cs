using System;
using Microsoft.EntityFrameworkCore;
using RaremintShop.Data;
using RaremintShop.Models;
using RaremintShop.Repositories;

namespace RaremintShop.Tests.Helpers
{
    /// <summary>
    /// OrderDetailテスト用のヘルパークラス
    /// </summary>
    public static class OrderDetailTestHelper
    {
        /// <summary>
        /// 新しいDbContextOptionsを作成し、一意のInMemoryDatabaseを使用する
        /// </summary>
        /// <returns>新しいDbContextOptions</returns>
        public static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        /// <summary>
        /// テスト用のOrderDetailオブジェクトを作成する
        /// </summary>
        /// <param name="orderId">注文ID</param>
        /// <param name="order">Orderオブジェクト</param>
        /// <param name="productId">商品ID</param>
        /// <param name="product">Productオブジェクト</param>
        /// <param name="quantity">数量</param>
        /// <param name="price">単価</param>
        /// <param name="createdAt">作成日時</param>
        /// <param name="updatedAt">更新日時</param>
        /// <returns>新しいOrderDetailオブジェクト</returns>
        public static OrderDetail CreateTestOrderDetail(
            int orderId,
            Order order,
            int productId,
            Product product,
            int quantity = 1,
            decimal price = 10.0m,
            DateTime? createdAt = null,
            DateTime? updatedAt = null)
        {
            return new OrderDetail(orderId, order, productId, product, quantity, price, createdAt, updatedAt);
        }

        /// <summary>
        /// テスト用のOrderDetailRepositoryを作成する
        /// </summary>
        /// <param name="context">出力されるApplicationDbContext</param>
        /// <returns>新しいOrderDetailRepository</returns>
        public static OrderDetailRepository CreateOrderDetailRepository(out ApplicationDbContext context)
        {
            var options = CreateNewContextOptions();
            context = new ApplicationDbContext(options);
            return new OrderDetailRepository(context);
        }
    }
}
