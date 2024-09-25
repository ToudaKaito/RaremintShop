//using System;
//using RaremintShop.Models;
//using RaremintShop.Tests.Helpers;
//using Xunit;

//namespace RaremintShop.Tests.Models
//{
//    /// <summary>
//    /// Productモデルのユニットテストクラス
//    /// </summary>
//    public class ProductTests
//    {
//        /// <summary>
//        /// 有効なデータでProductを作成できるかテストします。
//        /// </summary>
//        [Fact]
//        public void CanCreateProduct_WithValidData()
//        {
//            // Arrange
//            var productName = "TestProduct";
//            var price = 99.99m;
//            var stock = 100;
//            var description = "A test product";
//            var category = "Electronics";
//            var createdAt = DateTime.Now;
//            var updatedAt = DateTime.Now;

//            // Act
//            var product = ProductTestHelper.CreateTestProduct(productName, price, stock, description, category, createdAt, updatedAt);

//            // Assert
//            Assert.Equal(productName, product.ProductName);
//            Assert.Equal(price, product.Price);
//            Assert.Equal(stock, product.Stock);
//            Assert.Equal(description, product.Description);
//            Assert.Equal(category, product.Category);
//            Assert.Equal(createdAt, product.CreatedAt);
//            Assert.Equal(updatedAt, product.UpdatedAt);
//        }

//        /// <summary>
//        /// Productの詳細を更新できるかテストします。
//        /// </summary>
//        [Fact]
//        public void CanUpdateProductDetails()
//        {
//            // Arrange
//            var originalCreatedAt = DateTime.Now.AddDays(-1); // 作成日時を前日に設定
//            var product = ProductTestHelper.CreateTestProduct(createdAt: originalCreatedAt);

//            var newProductName = "NewProduct";
//            var newPrice = 199.99m;
//            var newStock = 50;
//            var newDescription = "An updated test product";
//            var newCategory = "Gadgets";
//            var newUpdatedAt = DateTime.Now;

//            // Act
//            product.ProductName = newProductName;
//            product.Price = newPrice;
//            product.Stock = newStock;
//            product.Description = newDescription;
//            product.Category = newCategory;
//            product.UpdatedAt = newUpdatedAt;

//            // Assert
//            Assert.Equal(newProductName, product.ProductName);
//            Assert.Equal(newPrice, product.Price);
//            Assert.Equal(newStock, product.Stock);
//            Assert.Equal(newDescription, product.Description);
//            Assert.Equal(newCategory, product.Category);
//            Assert.Equal(originalCreatedAt, product.CreatedAt);
//            Assert.Equal(newUpdatedAt, product.UpdatedAt);
//        }
//    }
//}
