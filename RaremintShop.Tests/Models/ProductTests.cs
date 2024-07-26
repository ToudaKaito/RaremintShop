using System;
using RaremintShop.Models;
using Xunit;

namespace RaremintShop.Tests.Models
{
    /// <summary>
    /// Productモデルのユニットテストクラス
    /// </summary>
    public class ProductTests
    {
        [Fact]
        public void CanCreateProduct_WithValidDate()
        {
            // Arrange
            var productName = "TestProduct";
            var category = "Electronics";
            var price = 99.99m;
            var stock = 100;
            var description = "A test product";
            var createdAt = DateTime.Now;
            var updatedAt = DateTime.Now;

            // Act
            var product = new Product
            {
                ProductName = productName,
                Category = category,
                Price = price,
                Stock = stock,
                Description = description,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt
            };

            // Assert
            Assert.Equal(productName, product.ProductName);
            Assert.Equal(category, product.Category);
            Assert.Equal(price, product.Price);
            Assert.Equal(stock, product.Stock);
            Assert.Equal(description, product.Description);
            Assert.Equal(createdAt, product.CreatedAt);
            Assert.Equal(updatedAt, product.UpdatedAt);
        }

        [Fact]
        public void CanUpdateProductDetails()
        {
            // Arrange
            var product = new Product
            {
                ProductName = "OldProduct",
                Category = "Electronics",
                Price = 49.99m,
                Stock = 200,
                Description = "An old test product",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var newProductName = "NewProduct";
            var newCategory = "Gadgets";
            var newPrice = 199.99m;
            var newStock = 50;
            var newDescription = "An updated test product";
            var newUpdatedAt = DateTime.Now;

            // Act
            product.ProductName = newProductName;
            product.Category = newCategory;
            product.Price = newPrice;
            product.Stock = newStock;
            product.Description = newDescription;
            product.UpdatedAt = newUpdatedAt;

            // Assert
            Assert.Equal(newProductName, product.ProductName);
            Assert.Equal(newCategory, product.Category);
            Assert.Equal(newPrice, product.Price);
            Assert.Equal(newStock, product.Stock);
            Assert.Equal(newDescription, product.Description);
            Assert.Equal(newUpdatedAt, product.UpdatedAt);
        }
    }
}
