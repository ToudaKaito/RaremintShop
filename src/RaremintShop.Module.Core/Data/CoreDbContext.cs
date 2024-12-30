using Microsoft.EntityFrameworkCore;

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

        
    }
}