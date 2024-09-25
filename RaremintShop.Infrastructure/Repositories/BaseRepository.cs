using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RaremintShop.Infrastructure.Repositories
{
    /// <summary>
    /// 各リポジトリが継承する基本クラス。
    /// データベースコンテキストとロギング機能を提供する。
    /// </summary>
    /// <typeparam name="TContext">使用するデータベースコンテキストの型</typeparam>
    public abstract class BaseRepository<TContext> where TContext : DbContext
    {
        protected readonly TContext _context;
        protected readonly ILogger _logger;

        /// <summary>
        /// コンストラクタ。データベースコンテキストとロガーのインスタンスを受け取り、初期化する。
        /// </summary>
        /// <param name="context">操作対象のデータベースコンテキスト</param>
        /// <param name="logger">操作のログを記録するためのロガー</param>
        protected BaseRepository(TContext context, ILogger logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// データベース操作中に発生した例外を処理し、適切なエラーメッセージを投げる。
        /// </summary>
        /// <param name="ex">発生した例外</param>
        /// <param name="actionDescription">操作の説明</param>
        /// <param name="entityId">操作対象のエンティティID（オプション）</param>
        protected void HandleException(Exception ex, string actionDescription, int? entityId = null)
        {
            // データベース更新エラーが発生した場合の処理
            if (ex is DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "{Action}中にデータベースエラーが発生しました。ID: {EntityId}", actionDescription, entityId);
                throw new Exception($"{actionDescription}中に問題が発生しました。もう一度お試しください。", dbUpdateEx);
            }
            // データベース競合エラーが発生した場合の処理
            else if (ex is DbUpdateConcurrencyException concurrencyEx)
            {
                _logger.LogError(concurrencyEx, "{Action}中に競合が発生しました。ID: {EntityId}", actionDescription, entityId);
                throw new Exception($"{actionDescription}中に競合が発生しました。もう一度お試しください。", concurrencyEx);
            }
            // その他の予期しないエラーが発生した場合の処理
            else
            {
                _logger.LogError(ex, "{Action}中に予期しないエラーが発生しました。", actionDescription);
                throw new Exception($"{actionDescription}中に予期しないエラーが発生しました。", ex);
            }
        }
    }
}
