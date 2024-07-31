using System;
using RaremintShop.Models;
using RaremintShop.Tests.Helpers;
using Xunit;

namespace RaremintShop.Tests.Models
{
    /// <summary>
    /// Productモデルのユニットテストクラス
    /// </summary>
    public class ProductTests
    {
        /// <summary>
        /// 有効なデータでProductを作成できるかテストします。
        /// </summary>
        [Fact]
        public void CanCreateProduct_WithValidData()
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
            var product = ProductTestHelper.CreateTestProduct(
                productName: productName,
                category: category,
                price: price,
                stock: stock,
                description: description,
                createdAt: createdAt,
                updatedAt: updatedAt
            );

            // Assert
            Assert.Equal(productName, product.ProductName);
            Assert.Equal(category, product.Category);
            Assert.Equal(price, product.Price);
            Assert.Equal(stock, product.Stock);
            Assert.Equal(description, product.Description);
            Assert.Equal(createdAt, product.CreatedAt);
            Assert.Equal(updatedAt, product.UpdatedAt);
        }

        /// <summary>
        /// Productの詳細を更新できるかテストします。
        /// </summary>
        [Fact]
        public void CanUpdateProductDetails()
        {
            // Arrange
            var originalProductName = "OldProduct";
            var originalCategory = "Electronics";
            var originalPrice = 49.99m;
            var originalStock = 200;
            var originalDescription = "An old test product";
            var originalCreatedAt = DateTime.Now;
            var originalUpdatedAt = DateTime.Now;

            var product = ProductTestHelper.CreateTestProduct(
                productName: originalProductName,
                category: originalCategory,
                price: originalPrice,
                stock: originalStock,
                description: originalDescription,
                createdAt: originalCreatedAt,
                updatedAt: originalUpdatedAt
            );

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
