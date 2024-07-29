using System;
using RaremintShop.Models;
using Xunit;

namespace RaremintShop.Tests.Models
{
    /// <summary>
    /// Orderモデルのユニットテストクラス
    /// </summary>
    public class OrderTests
    {
        [Fact]
        public void CanCreateOrder_WithValidData()
        {
            // Arrange
            var userId = 1;
            var orderDate = DateTime.Now;
            var totalAmount = 299.99m;
            var status = "Pending";
            var createdAt = DateTime.Now;
            var updatedAt = DateTime.Now;

            // Act
            var order = new Order
            {
                UserID = userId,
                OrderDate = orderDate,
                TotalAmount = totalAmount,
                Status = status,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt
            };

            // Assert
            Assert.Equal(userId, order.UserID);
            Assert.Equal(orderDate, order.OrderDate);
            Assert.Equal(totalAmount, order.TotalAmount);
            Assert.Equal(status, order.Status);
            Assert.Equal(createdAt, order.CreatedAt);
            Assert.Equal(updatedAt, order.UpdatedAt);
        }

        [Fact]
        public void CanUpdateOrderDetails()
        {
            // Arrange
            var order = new Order
            {
                UserID = 1,
                OrderDate = DateTime.Now,
                TotalAmount = 299.99m,
                Status = "Pending",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var newUserId = 2;
            var newOrderDate = DateTime.Now;
            var newTotalAmount = 399.99m;
            var newStatus = "Completed";
            var newUpdatedAt = DateTime.Now;

            // Act
            order.UserID = newUserId;
            order.OrderDate = newOrderDate;
            order.TotalAmount = newTotalAmount;
            order.Status = newStatus;
            order.UpdatedAt = newUpdatedAt;

            // Assert
            Assert.Equal(newUserId, order.UserID);
            Assert.Equal(newOrderDate, order.OrderDate);
            Assert.Equal(newTotalAmount, order.TotalAmount);
            Assert.Equal(newStatus, order.Status);
            Assert.Equal(newUpdatedAt, order.UpdatedAt);
        }
    }
}
