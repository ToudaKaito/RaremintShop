using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RaremintShop.Data;
using RaremintShop.Models;
using RaremintShop.Repositories;
using RaremintShop.Tests.Helpers;
using Xunit;

namespace RaremintShop.Tests.Repositories
{
    /// <summary>
    /// ProductRepositoryのユニットテストクラス
    /// </summary>
    public class ProductRepositoryTests
    {
        private ApplicationDbContext _context = null!;
        private ProductRepository _productRepository = null!;

        /// <summary>
        /// テストデータベースを初期化します。
        /// </summary>
        private void InitializeTestDatabase()
        {
            _productRepository = ProductTestHelper.CreateProductRepository(out _context);
        }

        /// <summary>
        /// 商品IDによる商品の取得をテストします。
        /// </summary>
        [Fact]
        public void GetProductById_ProductExists_ReturnsProduct()
        {
            // Arrange
            InitializeTestDatabase();
            var product = ProductTestHelper.CreateTestProduct();
            _context.Products.Add(product);
            _context.SaveChanges();

            // Act
            var result = _productRepository.GetProductById(product.ProductID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.ProductID,result.ProductID);
            Assert.Equal(product.ProductName,result.ProductName);
            Assert.Equal(product.Price,result.Price);
            Assert.Equal(product.Stock,result.Stock);
            Assert.Equal(product.Description,result.Description);
            Assert.Equal(product.Category,result.Category);
            Assert.Equal(product.CreatedAt,result.CreatedAt);
            Assert.Equal(product.UpdatedAt,result.UpdatedAt);
        }

        /// <summary>
        /// キーワードとカテゴリーによる商品の検索をテストします。
        /// </summary>
        [Fact]
        public void SearchProducts_ByKeywordAndCategory_ReturnsProducts()
        {
            // Arrange
            InitializeTestDatabase();
            var product1 = ProductTestHelper.CreateTestProduct("TestProduct1", category: "Category1");
            var product2 = ProductTestHelper.CreateTestProduct("TestProduct2", category: "Category2");
            _context.Products.AddRange(product1,product2);
            _context.SaveChanges();

            // Act
            var result = _productRepository.SearchProducts("TestProduct1", "Category1").ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal(product1.ProductID, result[0].ProductID);
        }

        /// <summary>
        /// 有効な商品の追加をテストします。
        /// </summary>
        [Fact]
        public void AddProduct_ValidProduct_AddsProduct()
        {
            // Arrange
            InitializeTestDatabase();
            var product = ProductTestHelper.CreateTestProduct();

            // Act
            _productRepository.AddProduct(product);
            var result = _context.Products.Find(product.ProductID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.ProductID, result.ProductID);
            Assert.Equal(product.ProductName, result.ProductName);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.Stock, result.Stock);
            Assert.Equal(product.Description, result.Description);
            Assert.Equal(product.Category, result.Category);
            Assert.Equal(product.CreatedAt, result.CreatedAt);
            Assert.Equal(product.UpdatedAt, result.UpdatedAt);
        }

        /// <summary>
        /// 有効な商品の更新をテストします。
        /// </summary>
        [Fact]
        public void UpdateProduct_ValidProduct_UpdatesProduct()
        {
            // Arrange
            InitializeTestDatabase();
            var originalCreatedAt = DateTime.Now.AddDays(-1);
            var product = ProductTestHelper.CreateTestProduct(createdAt: originalCreatedAt);
            _context.Products.Add(product);
            _context.SaveChanges();

            // 元のエンティティをデタッチ
            // デタッチすることで、同じキー値（ProductID）を持つ別のエンティティ（updatedProduct）をトラッキングできるようにします。
            // これにより、同じProductIDを持つエンティティが2つトラッキングされることで発生する競合エラーを防ぎます。
            _context.Entry(product).State = EntityState.Detached;

            var updatedProduct = ProductTestHelper.CreateTestProduct("UpdatedProduct", 20.0m, 200, "UpdatedDescription", "UpdatedCategory", createdAt: originalCreatedAt);

            // Act
            _productRepository.UpdateProduct(updatedProduct);
            var result = _context.Products.Find(updatedProduct.ProductID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedProduct.ProductID, result.ProductID);
            Assert.Equal(updatedProduct.ProductName, result.ProductName);
            Assert.Equal(updatedProduct.Price, result.Price);
            Assert.Equal(updatedProduct.Stock, result.Stock);
            Assert.Equal(updatedProduct.Description, result.Description);
            Assert.Equal(updatedProduct.Category, result.Category);
            Assert.Equal(updatedProduct.CreatedAt, result.CreatedAt);
            Assert.Equal(updatedProduct.UpdatedAt, result.UpdatedAt);
        }
    }
}
