using Microsoft.EntityFrameworkCore;
using RaremintShop.Module.Catalog.Models;

namespace RaremintShop.Module.Catalog.Data
{
    /// <summary>
    /// カタログモジュールのデータベースコンテキストクラス。
    /// Catalogモジュールで使用されるエンティティのデータベース操作を管理する。
    /// </summary>
    public class CatalogDbContext : DbContext
    {
        /// <summary>
        /// コンストラクタ。データベースコンテキストに必要なオプションを受け取る。
        /// </summary>
        /// <param name="options">DbContext の設定オプション</param>
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 商品データにアクセスするための DbSet。
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// モデルの構成を行うメソッド。
        /// このメソッドを使ってエンティティの設定や制約を定義する。
        /// </summary>
        /// <param name="modelBuilder">モデルの構築を行う ModelBuilder インスタンス</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product エンティティの構成を設定
            ConfigureProductEntity(modelBuilder);
        }

        /// <summary>
        /// Product エンティティに対する詳細な構成を行うメソッド。
        /// インデックスの作成などのエンティティに対する設定を定義する。
        /// </summary>
        /// <param name="modelBuilder">エンティティモデルの構成を行う ModelBuilder</param>
        private void ConfigureProductEntity(ModelBuilder modelBuilder)
        {
            // Product エンティティに対して ProductName フィールドのインデックスを作成
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductName);
        }
    }
}