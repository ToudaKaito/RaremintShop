//using System;
//using RaremintShop.Models;
//using RaremintShop.Tests.Helpers;
//using Xunit;

//namespace RaremintShop.Tests.Models
//{
//    /// <summary>
//    /// OrderDetailモデルのユニットテストクラス
//    /// </summary>
//    public class OrderDetailTests
//    {
//        /// <summary>
//        /// 有効なデータでOrderDetailオブジェクトを作成できるかをテストします。
//        /// </summary>
//        [Fact]
//        public void CanCreateOrderDetail_WithValidData()
//        {
//            // Arrange
//            var user = UserTestHelper.CreateTestUser();
//            var product = ProductTestHelper.CreateTestProduct();
//            var order = OrderTestHelper.CreateTestOrder(user.UserID, user);
//            var quantity = 2;
//            var price = 50.0m;
//            var createdAt = DateTime.Now;
//            var updatedAt = DateTime.Now;

//            // Act
//            var orderDetail = new OrderDetail
//            {
//                OrderID = order.OrderID,
//                ProductID = product.ProductID,
//                Order = order,
//                Product = product,
//                Quantity = quantity,
//                Price = price,
//                CreatedAt = createdAt,
//                UpdatedAt = updatedAt
//            };

//            // Assert
//            Assert.Equal(order.OrderID, orderDetail.OrderID);
//            Assert.Equal(product.ProductID, orderDetail.ProductID);
//            Assert.Equal(order, orderDetail.Order);
//            Assert.Equal(product, orderDetail.Product);
//            Assert.Equal(quantity, orderDetail.Quantity);
//            Assert.Equal(price, orderDetail.Price);
//            Assert.Equal(createdAt, orderDetail.CreatedAt);
//            Assert.Equal(updatedAt, orderDetail.UpdatedAt);
//        }

//        /// <summary>
//        /// OrderDetailオブジェクトの詳細を更新できるかをテストします。
//        /// </summary>
//        [Fact]
//        public void CanUpdateOrderDetailDetails()
//        {
//            // Arrange
//            var user = UserTestHelper.CreateTestUser();
//            var product = ProductTestHelper.CreateTestProduct();
//            var order = OrderTestHelper.CreateTestOrder(user.UserID, user);
//            var orderDetail = new OrderDetail(order.OrderID, order, product.ProductID, product, 1, 100.0m);

//            var newQuantity = 3;
//            var newPrice = 150.0m;
//            var newUpdatedAt = DateTime.Now;

//            // Act
//            orderDetail.Quantity = newQuantity;
//            orderDetail.Price = newPrice;
//            orderDetail.UpdatedAt = newUpdatedAt;

//            // Assert
//            Assert.Equal(newQuantity, orderDetail.Quantity);
//            Assert.Equal(newPrice, orderDetail.Price);
//            Assert.Equal(newUpdatedAt, orderDetail.UpdatedAt);
//        }
//    }
//}
