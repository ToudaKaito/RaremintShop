using System;
using Microsoft.EntityFrameworkCore;
using RaremintShop.Data;
using RaremintShop.Models;
using RaremintShop.Repositories;

namespace RaremintShop.Tests.Helpers
{
    /// <summary>
    /// テスト用のProductヘルパークラス
    /// </summary>
    public static class ProductTestHelper
    {
        /// <summary>
        /// InMemoryDatabase用の新しいDbContextOptionsを作成します。
        /// </summary>
        /// <returns>新しいDbContextOptions</returns>
        public static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        /// <summary>
        /// テスト用のProductオブジェクトを作成します。
        /// </summary>
        /// <param name="id">プロダクトID</param>
        /// <param name="productName">プロダクト名</param>
        /// <param name="price">価格</param>
        /// <param name="stock">在庫数</param>
        /// <param name="description">説明</param>
        /// <param name="category">カテゴリ</param>
        /// <param name="createdAt">作成日時（オプション）</param>
        /// <param name="updatedAt">更新日時（オプション）</param>
        /// <returns>新しいProductオブジェクト</returns>
        public static Product CreateTestProduct(int id = 1, string productName = "TestProduct", decimal price = 10.0m, int stock = 100, string description = "Description", string category = "General", DateTime? createdAt = null, DateTime? updatedAt = null)
        {
            return new Product
            {
                ProductID = id,
                ProductName = productName,
                Price = price,
                Stock = stock,
                Description = description,
                Category = category,
                CreatedAt = createdAt ?? DateTime.Now,
                UpdatedAt = updatedAt ?? DateTime.Now
            };
        }

        /// <summary>
        /// テスト用のProductRepositoryを作成します。
        /// </summary>
        /// <param name="context">ApplicationDbContextのインスタンスを出力</param>
        /// <returns>新しいProductRepository</returns>
        public static ProductRepository CreateProductRepository(out ApplicationDbContext context)
        {
            var options = CreateNewContextOptions();
            context = new ApplicationDbContext(options);
            return new ProductRepository(context);
        }
    }
}
