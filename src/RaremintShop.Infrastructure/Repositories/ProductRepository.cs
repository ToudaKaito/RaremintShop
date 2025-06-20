﻿using Microsoft.EntityFrameworkCore;
using RaremintShop.Core.Interfaces.Repositories;
using RaremintShop.Core.Models;
using RaremintShop.Infrastructure.Data;

namespace RaremintShop.Infrastructure.Repositories
{
    /// <summary>
    /// 商品情報を管理するリポジトリの実装クラス
    /// </summary>
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        /// <summary>
        /// コンストラクタ。データベースコンテキストを受け取ります。
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbSet.AnyAsync(p => p.Name == name);
        }

    }
}