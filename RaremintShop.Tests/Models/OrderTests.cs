//using System;
//using RaremintShop.Models;
//using RaremintShop.Tests.Helpers;
//using Xunit;

//namespace RaremintShop.Tests.Models
//{
//    /// <summary>
//    /// Orderモデルのユニットテストクラス
//    /// </summary>
//    public class OrderTests
//    {
//        /// <summary>
//        /// 有効なデータでOrderオブジェクトを作成できるかをテストします。
//        /// </summary>
//        [Fact]
//        public void CanCreateOrder_WithValidData()
//        {
//            // Arrange
//            var user = UserTestHelper.CreateTestUser();
//            var orderDate = DateTime.Now;
//            var totalAmount = 299.99m;
//            var status = "Pending";
//            var createdAt = DateTime.Now;
//            var updatedAt = DateTime.Now;

//            // Act
//            var order = OrderTestHelper.CreateTestOrder(user.UserID, user, orderDate, totalAmount, status, createdAt, updatedAt);        

//            // Assert
//            Assert.Equal(user.UserID, order.UserID);
//            Assert.Equal(user, order.User);
//            Assert.Equal(orderDate, order.OrderDate);
//            Assert.Equal(totalAmount, order.TotalAmount);
//            Assert.Equal(status, order.Status);
//            Assert.Equal(createdAt, order.CreatedAt);
//            Assert.Equal(updatedAt, order.UpdatedAt);
//        }

//        /// <summary>
//        /// Orderオブジェクトの詳細を更新できるかをテストします。
//        /// </summary>
//        [Fact]
//        public void CanUpdateOrderDetails()
//        {
//            // Arrange
//            var user = UserTestHelper.CreateTestUser();
//            var originalCreatedAt = DateTime.Now.AddDays(-1); // 作成日時を前日に設定
//            var order = OrderTestHelper.CreateTestOrder(user.UserID, user, createdAt: originalCreatedAt);

//            var newOrderDate = DateTime.Now;
//            var newTotalAmount = 399.99m;
//            var newStatus = "Completed";
//            var newUpdatedAt = DateTime.Now;

//            // Act
//            order.OrderDate = newOrderDate;
//            order.TotalAmount = newTotalAmount;
//            order.Status = newStatus;
//            order.UpdatedAt = newUpdatedAt;

//            // Assert
//            Assert.Equal(user.UserID, order.UserID);
//            Assert.Equal(user, order.User);
//            Assert.Equal(newOrderDate, order.OrderDate);
//            Assert.Equal(newTotalAmount, order.TotalAmount);
//            Assert.Equal(newStatus, order.Status);
//            Assert.Equal(originalCreatedAt, order.CreatedAt);
//            Assert.Equal(newUpdatedAt, order.UpdatedAt);
//        }
//    }
//}
