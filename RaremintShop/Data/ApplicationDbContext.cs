using Microsoft.EntityFrameworkCore;
using RaremintShop.Models;

namespace RaremintShop.Data
{
    /// <summary>
    /// アプリケーションのデータベースコンテキストを表します。
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// <see cref="ApplicationDbContext"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="options"><see cref="DbContext"/> に使用するオプション。</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSetプロパティは、コンテキスト内の指定されたエンティティのコレクションを表します。
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// モデルが作成される際に呼び出されます。
        /// </summary>
        /// <param name="modelBuilder">このコンテキストのモデルを構築するために使用されるビルダー。</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 各エンティティの設定
            ConfigureUserEntity(modelBuilder);
            ConfigureProductEntity(modelBuilder);
            ConfigureOrderEntity(modelBuilder);
            ConfigureOrderDetailEntity(modelBuilder);
        }

        /// <summary>
        /// Userエンティティを設定します。
        /// </summary>
        /// <param name="modelBuilder">モデルビルダー</param>
        private void ConfigureUserEntity(ModelBuilder modelBuilder)
        {
            // UserテーブルのEmailに一意制約を追加
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        /// <summary>
        /// Productエンティティを設定します。
        /// </summary>
        /// <param name="modelBuilder">モデルビルダー</param>
        private void ConfigureProductEntity(ModelBuilder modelBuilder)
        {
            // ProductテーブルのProductNameにインデックスを追加
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductName);
        }

        /// <summary>
        /// Orderエンティティを設定します。
        /// </summary>
        /// <param name="modelBuilder">モデルビルダー</param>
        private void ConfigureOrderEntity(ModelBuilder modelBuilder)
        {
            // OrderテーブルとUserの外部キー関係を設定
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.Restrict);
        }

        /// <summary>
        /// OrderDetailエンティティを設定します。
        /// </summary>
        /// <param name="modelBuilder">モデルビルダー</param>
        private void ConfigureOrderDetailEntity(ModelBuilder modelBuilder)
        {
            // OrderDetailに複合キーを設定
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderID, od.ProductID });

            // OrderDetailとOrderの外部キー関係を設定
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderDetailとProductの外部キー関係を設定
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
