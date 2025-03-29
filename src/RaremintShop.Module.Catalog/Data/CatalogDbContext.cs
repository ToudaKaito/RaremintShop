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
        public DbSet<Product> Products { get; set; } = null!;

        /// <summary>
        /// カテゴリデータにアクセスするための DbSet。
        /// </summary>
        public DbSet<Category> Categories { get; set; } = null!;

        /// <summary>
        /// 商品画像データにアクセスするための DbSet。
        /// </summary>
        public DbSet<ProductImage> ProductImages { get; set; } = null!;

        /// <summary>
        /// モデルの構成を行うメソッド。
        /// このメソッドを使ってエンティティの設定や制約を定義する。
        /// </summary>
        /// <param name="modelBuilder">モデルの構築を行う ModelBuilder インスタンス</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 商品テーブルの設定
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); // 価格のデータ型をdecimal(18,2)に設定

            modelBuilder.Entity<Product>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP"); // 作成日時のデフォルト値を現在のタイムスタンプに設定

            modelBuilder.Entity<Product>()
                .Property(p => p.IsPublished)
                .HasDefaultValue(true); // 公開フラグのデフォルト値をtrueに設定

            // 商品画像テーブルの設定
            modelBuilder.Entity<ProductImage>()
                .Property(pi => pi.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP"); // 作成日時のデフォルト値を現在のタイムスタンプに設定

            modelBuilder.Entity<ProductImage>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // 商品が削除されたときに関連する画像も削除

            // カテゴリテーブルの設定
            modelBuilder.Entity<Category>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP"); // 作成日時のデフォルト値を現在のタイムスタンプに設定

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // カテゴリが削除されたときに関連する商品は削除しない
        }

    }
}