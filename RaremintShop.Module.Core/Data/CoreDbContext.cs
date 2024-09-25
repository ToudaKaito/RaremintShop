using Microsoft.EntityFrameworkCore;
using RaremintShop.Module.Core.Models;

namespace RaremintShop.Module.Core.Data
{
    /// <summary>
    /// Coreモジュールのデータベースコンテキストクラス。
    /// Coreモジュールで使用されるエンティティのデータベース操作を管理する。
    /// </summary>
    public class CoreDbContext : DbContext
    {
        /// <summary>
        /// コンストラクタ。データベースコンテキストに必要なオプションを受け取る。
        /// </summary>
        /// <param name="options">DbContext の設定オプション</param>
        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// ユーザーデータにアクセスするための DbSet。
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// モデルの構成を行うメソッド。
        /// このメソッドを使ってエンティティの設定や制約を定義する。
        /// </summary>
        /// <param name="modelBuilder">モデルの構築を行う ModelBuilder インスタンス</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User エンティティの構成を設定
            ConfigureUserEntity(modelBuilder);
        }

        /// <summary>
        /// User エンティティに対する詳細な構成を行うメソッド。
        /// Emailフィールドにユニークインデックスを設定する。
        /// </summary>
        /// <param name="modelBuilder">エンティティモデルの構成を行う ModelBuilder</param>
        private void ConfigureUserEntity(ModelBuilder modelBuilder)
        {
            // User エンティティに対して Email フィールドのユニークインデックスを作成
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}