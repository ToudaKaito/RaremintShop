using System;
using System.Collections.Generic;
using System.Linq;
using RaremintShop.Data;
using RaremintShop.Models;

namespace RaremintShop.Repositories
{
    /// <summary>
    /// 商品情報を管理するリポジトリの実装クラス
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// コンストラクタ。データベースコンテキストを受け取ります。
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// 商品IDに基づいて商品を取得します。
        /// </summary>
        /// <param name="productId">取得する商品のID</param>
        /// <returns>商品エンティティ</returns>
        public Product GetProductById(int productId)
        {
            var product = _context.Products.Find(productId);
            if(product == null)
            {
                throw new NullReferenceException($"Product with ID {productId} not found.");
            }
            return product;
        }

        /// <summary>
        /// キーワードとカテゴリーに基づいて商品を検索します。
        /// </summary>
        /// <param name="keyword">検索するキーワード</param>
        /// <param name="category">検索するカテゴリー</param>
        /// <returns>商品のリスト</returns>
        public IEnumerable<Product> SearchProducts(string keyword,string category)
        {
            return _context.Products
                .Where(p => (string.IsNullOrEmpty(keyword) || p.ProductName.Contains(keyword))
                && (string.IsNullOrEmpty(category) || p.Category == category))
                .ToList();
        }

        /// <summary>
        /// 新しい商品を追加します。
        /// </summary>
        /// <param name="product">追加する商品エンティティ</param>
        public void AddProduct(Product product)
        {
            if(product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        /// <summary>
        /// 既存の商品情報を更新します。
        /// </summary>
        /// <param name="product">更新する商品エンティティ</param>
        public void UpdateProduct(Product product)
        {
            if(product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
