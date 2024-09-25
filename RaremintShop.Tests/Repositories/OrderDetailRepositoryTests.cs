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
//    /// OrderDetailRepositoryのユニットテストクラス
//    /// </summary>
//    public class OrderDetailRepositoryTests
//    {
//        private ApplicationDbContext _context = null!;
//        private OrderDetailRepository _orderDetailRepository = null!;

//        /// <summary>
//        /// テストデータベースを初期化します。
//        /// </summary>
//        private void InitializeTestDatabase()
//        {
//            _orderDetailRepository = OrderDetailTestHelper.CreateOrderDetailRepository(out _context);

//            // 前提条件:User,Order,Productを作成する
//            var user = UserTestHelper.CreateTestUser();
//            _context.Users.Add(user);
//            var order = OrderTestHelper.CreateTestOrder(userId: user.UserID, user: user);
//            _context.Orders.Add(order);
//            var product = ProductTestHelper.CreateTestProduct();
//            _context.Products.Add(product);
//            _context.SaveChanges();
//        }

//        /// <summary>
//        /// 注文IDと商品IDによる注文詳細の取得をテストします。
//        /// </summary>
//        [Fact]
//        public void GetOrderDetailById_OrderDetailExists_ReturnOrderDetail()
//        {
//            // Arrange
//            InitializeTestDatabase();
//            var order = _context.Orders.First();
//            var product = _context.Products.First();
//            var orderDetail = OrderDetailTestHelper.CreateTestOrderDetail(order.OrderID, order, product.ProductID, product);
//            _context.OrderDetails.Add(orderDetail);
//            _context.SaveChanges();

//            // Act
//            var result = _orderDetailRepository.GetOrderDetailById(orderDetail.OrderID, orderDetail.ProductID);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(orderDetail.OrderID, result.OrderID);
//            Assert.Equal(orderDetail.ProductID, result.ProductID);
//            Assert.Equal(orderDetail.Quantity, result.Quantity);
//            Assert.Equal(orderDetail.Price, result.Price);
//            Assert.Equal(orderDetail.CreatedAt, result.CreatedAt);
//            Assert.Equal(orderDetail.UpdatedAt, result.UpdatedAt);
//        }

//        /// <summary>
//        /// 注文IDによる注文詳細のリスト取得をテストします。
//        /// </summary>
//        [Fact]
//        public void GetOrderDetailsByOrderId_OrderHasOrderDetails_ReturnOrderDetails()
//        {
//            // Arrange
//            InitializeTestDatabase();
//            var user = _context.Users.First();// データベースから最初のユーザーを取得
//            var order = OrderTestHelper.CreateTestOrder(userId: user.UserID, user: user);
//            _context.Orders.Add(order);
//            _context.SaveChanges();

//            var product1 = ProductTestHelper.CreateTestProduct();
//            var product2 = ProductTestHelper.CreateTestProduct();
//            _context.Products.Add(product1);
//            _context.Products.Add(product2);
//            _context.SaveChanges();

//            var orderDetail1 = OrderDetailTestHelper.CreateTestOrderDetail(order.OrderID, order, product1.ProductID, product1);
//            var orderDetail2 = OrderDetailTestHelper.CreateTestOrderDetail(order.OrderID, order, product2.ProductID, product2);

//            _context.OrderDetails.Add(orderDetail1);
//            _context.SaveChanges();

//            _context.Entry(orderDetail1).State = EntityState.Detached;

//            _context.OrderDetails.Add(orderDetail2);
//            _context.SaveChanges();

//            // Act
//            var result = _orderDetailRepository.GetOrderDetailsByOrderId(order.OrderID).ToList();

//            // Assert
//            var expectedCount = 2;
//            Assert.Equal(expectedCount, result.Count);
//            Assert.Contains(result, od => od.OrderID == orderDetail1.OrderID && od.ProductID == orderDetail1.ProductID);
//            Assert.Contains(result, od => od.OrderID == orderDetail2.OrderID && od.ProductID == orderDetail2.ProductID);
//        }

//        /// <summary>
//        /// 有効な注文詳細の追加をテストします。
//        /// </summary>
//        [Fact]
//        public void AddOrderDetail_ValidOrderDetail_AddsOrderDetail()
//        {
//            // Arrange
//            InitializeTestDatabase();
//            var order = _context.Orders.First();
//            var product = _context.Products.First();
//            var orderDetail = OrderDetailTestHelper.CreateTestOrderDetail(order.OrderID, order, product.ProductID, product);

//            // Act
//            _orderDetailRepository.AddOrderDetail(orderDetail);
//            var result = _context.OrderDetails.Find(orderDetail.OrderID, orderDetail.ProductID);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(orderDetail.OrderID, result.OrderID);
//            Assert.Equal(orderDetail.ProductID, result.ProductID);
//            Assert.Equal(orderDetail.Quantity, result.Quantity);
//            Assert.Equal(orderDetail.Price, result.Price);
//            Assert.Equal(orderDetail.CreatedAt, result.CreatedAt);
//            Assert.Equal(orderDetail.UpdatedAt, result.UpdatedAt);
//        }

//        /// <summary>
//        /// 有効な注文詳細の更新をテストします。
//        /// </summary>
//        [Fact]
//        public void UpdateOrderDetail_ValidOrderDetail_UpdatesOrderDetail()
//        {
//            // Arrange
//            InitializeTestDatabase();
//            var order = _context.Orders.First();
//            var product = _context.Products.First();
//            var orderDetail = OrderDetailTestHelper.CreateTestOrderDetail(order.OrderID, order, product.ProductID, product, 1, 10.0m);
//            _context.OrderDetails.Add(orderDetail);
//            _context.SaveChanges();

//            // 元のエンティティをデタッチ
//            _context.Entry(orderDetail).State = EntityState.Detached;

//            var updatedOrderDetail = OrderDetailTestHelper.CreateTestOrderDetail(orderDetail.OrderID, order, orderDetail.ProductID, product, 2, 20.0m);

//            // Act
//            _orderDetailRepository.UpdateOrderDetail(updatedOrderDetail);
//            var result = _context.OrderDetails.Find(updatedOrderDetail.OrderID, updatedOrderDetail.ProductID);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(updatedOrderDetail.OrderID, result.OrderID);
//            Assert.Equal(updatedOrderDetail.ProductID, result.ProductID);
//            Assert.Equal(updatedOrderDetail.Quantity, result.Quantity);
//            Assert.Equal(updatedOrderDetail.Price, result.Price);
//            Assert.Equal(updatedOrderDetail.CreatedAt, result.CreatedAt);
//            Assert.Equal(updatedOrderDetail.UpdatedAt, result.UpdatedAt);
//        }
//    }
//}
