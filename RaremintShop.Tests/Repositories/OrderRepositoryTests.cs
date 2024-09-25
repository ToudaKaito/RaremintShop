//using System;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;
//using RaremintShop.Data;
//using RaremintShop.Models;
//using RaremintShop.Repositories;
//using RaremintShop.Tests.Helpers;
//using Xunit;

//namespace RaremintShop.Tests.Repositories
//{
//    /// <summary>
//    /// OrderRepositoryのユニットテストクラス
//    /// </summary>
//    public class OrderRepositoryTests
//    {
//        private ApplicationDbContext _context = null!;
//        private OrderRepository _orderRepository = null!;

//        /// <summary>
//        /// テストデータベースを初期化します。
//        /// </summary>
//        private void InitializeTestDatabase()
//        {
//            _orderRepository = OrderTestHelper.CreateOrderRepository(out _context);

//            // 前提条件:Userを作成する
//            var user = UserTestHelper.CreateTestUser();
//            _context.Users.Add(user);
//            _context.SaveChanges();
//        }

//        /// <summary>
//        /// 注文IDによる注文の取得をテストします。
//        /// </summary>
//        [Fact]
//        public void GetOrderById_OrderExists_ReturnOrder()
//        {
//            // Arrange
//            InitializeTestDatabase();
//            var user = _context.Users.First();
//            var order = OrderTestHelper.CreateTestOrder(userId: user.UserID, user: user);
//            _context.Orders.Add(order);
//            _context.SaveChanges();

//            // Act
//            var result = _orderRepository.GetOrderById(order.OrderID);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(order.OrderID, result.OrderID);
//            Assert.Equal(order.UserID, result.UserID);
//            Assert.Equal(order.OrderDate, result.OrderDate);
//            Assert.Equal(order.TotalAmount, result.TotalAmount);
//            Assert.Equal(order.Status, result.Status);
//            Assert.Equal(order.CreatedAt, result.CreatedAt);
//            Assert.Equal(order.UpdatedAt, result.UpdatedAt);
//        }

//        /// <summary>
//        /// ユーザーIDによる注文の取得をテストします。
//        /// </summary>
//        [Fact]
//        public void GetOrdersByUserId_UserHasOrders_ReturnOrders()
//        {
//            // Arrange
//            InitializeTestDatabase();
//            var user = _context.Users.First();
//            var order1 = OrderTestHelper.CreateTestOrder(userId: user.UserID, user: user);
//            var order2 = OrderTestHelper.CreateTestOrder(userId: user.UserID, user: user);
//            _context.Orders.Add(order1);
//            _context.Orders.Add(order2);
//            _context.SaveChanges();

//            // Act
//            var result = _orderRepository.GetOrdersByUserId(order1.UserID).ToList();

//            // Assert
//            var expectedCount = 2;
//            Assert.Equal(expectedCount, result.Count);
//            Assert.Contains(result, o => o.OrderID == order1.OrderID);
//            Assert.Contains(result, o => o.OrderID == order2.OrderID);
//        }

//        /// <summary>
//        /// 有効な注文の追加をテストします。
//        /// </summary>
//        [Fact]
//        public void AddOrder_ValidOrder_AddsOrder()
//        {
//            // Arrange
//            InitializeTestDatabase();
//            var user = _context.Users.First();
//            var order = OrderTestHelper.CreateTestOrder(userId: user.UserID, user: user);

//            // Act
//            _orderRepository.AddOrder(order);
//            var result = _context.Orders.Find(order.OrderID);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(order.OrderID, result.OrderID);
//            Assert.Equal(order.UserID, result.UserID);
//            Assert.Equal(order.OrderDate, result.OrderDate);
//            Assert.Equal(order.TotalAmount, result.TotalAmount);
//            Assert.Equal(order.Status, result.Status);
//            Assert.Equal(order.CreatedAt, result.CreatedAt);
//            Assert.Equal(order.UpdatedAt, result.UpdatedAt);
//        }

//        /// <summary>
//        /// 有効な注文の更新をテストします。
//        /// </summary>
//        [Fact]
//        public void UpdateOrder_ValidOrder_UpdatesOrder()
//        {
//            // Arrange
//            InitializeTestDatabase();
//            var user = _context.Users.First();
//            var originalCreatedAt = DateTime.Now.AddDays(-1);
//            var order = OrderTestHelper.CreateTestOrder(userId: user.UserID, user: user, createdAt: originalCreatedAt);
//            _context.Orders.Add(order);
//            _context.SaveChanges();

//            // 元のエンティティをデタッチ
//            // デタッチすることで、同じキー値（OrderID）を持つ別のエンティティ（updatedOrder）をトラッキングできるようにします。
//            // これにより、同じOrderIDを持つエンティティが2つトラッキングされることで発生する競合エラーを防ぎます。
//            _context.Entry(order).State = EntityState.Detached;

//            var updatedOrder = OrderTestHelper.CreateTestOrder(order.UserID, user, order.OrderDate, 200.0m, "Shipped", originalCreatedAt, DateTime.Now);

//            // Act
//            _orderRepository.UpdateOrder(updatedOrder);
//            var result = _context.Orders.Find(updatedOrder.OrderID);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(updatedOrder.OrderID, result.OrderID);
//            Assert.Equal(updatedOrder.UserID, result.UserID);
//            Assert.Equal(updatedOrder.OrderDate, result.OrderDate);
//            Assert.Equal(updatedOrder.TotalAmount, result.TotalAmount);
//            Assert.Equal(updatedOrder.Status, result.Status);
//            Assert.Equal(updatedOrder.CreatedAt, result.CreatedAt);
//            Assert.Equal(updatedOrder.UpdatedAt, result.UpdatedAt);
//        }
//    }
//}
