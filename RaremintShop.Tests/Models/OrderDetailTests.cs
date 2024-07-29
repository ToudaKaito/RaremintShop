using System;
using RaremintShop.Models;
using Xunit;

namespace RaremintShop.Tests.Models
{
    public class OrderDetailTests
    {
        [Fact]
        public void CanCreateOrderDetail_WithValidData()
        {
            // Arrange
            var orderId = 1;
            var productId = 1;
            var quantity = 2;
            var price = 49.99m;
            var createdAt = DateTime.Now;
            var updatedAt = DateTime.Now;

            // Act
            var orderDetail = new OrderDetail
            {
                OrderID = orderId,
                ProductID = productId,
                Quantity = quantity,
                Price = price,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt
            };

            // Assert
            Assert.Equal(orderId, orderDetail.OrderID);
            Assert.Equal(productId, orderDetail.ProductID);
            Assert.Equal(quantity, orderDetail.Quantity);
            Assert.Equal(price, orderDetail.Price);
            Assert.Equal(createdAt, orderDetail.CreatedAt);
            Assert.Equal(updatedAt, orderDetail.UpdatedAt);
        }

        [Fact]
        public void CanUpdateOrderDetailDetails()
        {
            // Arrange
            var orderDetail = new OrderDetail
            {
                OrderID = 1,
                ProductID = 1,
                Quantity = 2,
                Price = 49.99m,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var newQuantity = 5;
            var newPrice = 59.99m;
            var newUpdatedAt = DateTime.Now;

            // Act
            orderDetail.Quantity = newQuantity;
            orderDetail.Price = newPrice;
            orderDetail.UpdatedAt = newUpdatedAt;

            // Assert
            Assert.Equal(newQuantity, orderDetail.Quantity);
            Assert.Equal(newPrice, orderDetail.Price);
            Assert.Equal(newUpdatedAt, orderDetail.UpdatedAt);
        }
    }
}
