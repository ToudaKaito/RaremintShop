using System;
using Microsoft.EntityFrameworkCore;
using RaremintShop.Data;
using RaremintShop.Models;
using RaremintShop.Repositories;

namespace RaremintShop.Tests.Helpers
{
    /// <summary>
    /// Orderテスト用のヘルパークラス
    /// </summary>
    public static class OrderTestHelper
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
        /// テスト用のOrderオブジェクトを作成する
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="user">ユーザーオブジェクト</param>
        /// <param name="orderDate">注文日</param>
        /// <param name="totalAmount">合計金額</param>
        /// <param name="status">ステータス</param>
        /// <param name="createdAt">作成日時</param>
        /// <param name="updatedAt">更新日時</param>
        /// <returns>新しいOrderオブジェクト</returns>
        public static Order CreateTestOrder(
            int userId, 
            User user, 
            DateTime? orderDate = null, 
            decimal totalAmount = 100.0m, 
            string status = "Pending", 
            DateTime? createdAt = null, 
            DateTime? updatedAt = null)
        {
            return new Order(userId, user, orderDate, totalAmount, status, createdAt, updatedAt);
        }

        /// <summary>
        /// テスト用のOrderRepositoryを作成する
        /// </summary>
        /// <param name="context">出力されるApplicationDbContext</param>
        /// <returns>新しいOrderRepository</returns>
        public static OrderRepository CreateOrderRepository(out ApplicationDbContext context)
        {
            var options = CreateNewContextOptions();
            context = new ApplicationDbContext(options);
            return new OrderRepository(context);
        }
    }
}
