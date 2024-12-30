using Microsoft.EntityFrameworkCore;
using RaremintShop.Module.Orders.Models;

namespace RaremintShop.Module.Orders.Data
{
    /// <summary>
    /// Ordersモジュールのデータベースコンテキストクラス。
    /// Ordersモジュールで使用されるエンティティのデータベース操作を管理する。
    /// </summary>
    public class OrdersDbContext : DbContext
    {
        /// <summary>
        /// コンストラクタ。データベースコンテキストに必要なオプションを受け取る。
        /// </summary>
        /// <param name="options">DbContext の設定オプション</param>
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 注文データにアクセスするための DbSet。
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// 注文明細データにアクセスするための DbSet。
        /// </summary>
        public DbSet<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// モデルの構成を行うメソッド。
        /// エンティティに対する制約やインデックスを定義する。
        /// </summary>
        /// <param name="modelBuilder">モデルの構築を行う ModelBuilder インスタンス</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Order エンティティの構成
            ConfigureOrderEntity(modelBuilder);

            // OrderDetail エンティティの構成
            ConfigureOrderDetailEntity(modelBuilder);
        }

        /// <summary>
        /// Order エンティティの詳細な構成を行うメソッド。
        /// </summary>
        /// <param name="modelBuilder">エンティティモデルの構成を行う ModelBuilder</param>
        private void ConfigureOrderEntity(ModelBuilder modelBuilder)
        {
            // Order エンティティに対して UserID フィールドのインデックスを作成
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.UserID);
        }

        /// <summary>
        /// OrderDetail エンティティの詳細な構成を行うメソッド。
        /// </summary>
        /// <param name="modelBuilder">エンティティモデルの構成を行う ModelBuilder</param>
        private void ConfigureOrderDetailEntity(ModelBuilder modelBuilder)
        {
            // OrderDetail エンティティの複合キーを設定（OrderID と ProductID）
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderID, od.ProductID });

            // OrderID フィールドにインデックスを作成
            modelBuilder.Entity<OrderDetail>()
                .HasIndex(od => od.OrderID);
            // ProductID フィールドにインデックスを作成
            modelBuilder.Entity<OrderDetail>()
                .HasIndex(od => od.ProductID);
        }
    }
}