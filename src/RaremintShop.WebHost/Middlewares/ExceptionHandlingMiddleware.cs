using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace RaremintShop.WebHost.Middlewares
{
    /// <summary>
    /// アプリケーション全体で発生する例外をキャッチし、適切に処理するミドルウェア
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next; // 次のミドルウェアまたはエンドポイントを呼び出すデリゲート
        private readonly ILogger<ExceptionHandlingMiddleware> _logger; // ログを記録するためのロガー

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="next">次のミドルウェアを呼び出すデリゲート</param>
        /// <param name="logger">ロガーインスタンス</param>
        public ExceptionHandlingMiddleware(RequestDelegate next,ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// ミドルウェアのエントリポイント
        /// </summary>
        /// <param name="context">HTTPリクエストのコンテキスト</param>
        /// <returns>非同期タスク</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // 次のミドルウェアまたはエンドポイントを呼び出す
                await _next(context);
            }
            catch (Exception ex)
            {
                // 例外が発生した場合の処理
                _logger.LogError(ex, "未処理の例外が発生しました。"); // 例外をログに記録
                await HandleExceptionAsync(context, ex); // 例外を処理して適切なレスポンスを返す
            }
        }

        /// <summary>
        /// 発生した例外を処理し、HTTPレスポンスを生成するメソッド
        /// </summary>
        /// <param name="context">HTTPリクエストのコンテキスト</param>
        /// <param name="exception">発生した例外</param>
        /// <returns>非同期タスク</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // 例外の詳細を含むエラーレスポンスを作成
            var errorDetails = new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError, // HTTPステータスコード（500: Internal Server Error）
                Message = "内部サーバーエラーが発生しました。", // ユーザー向けのエラーメッセージ
                Detail = exception.Message // 例外の詳細（開発環境向け、本番環境では非表示にすることを推奨）
            };

            // エラーレスポンスをJSON形式にシリアライズ
            var response = JsonSerializer.Serialize(errorDetails);

            // HTTPレスポンスの設定
            context.Response.ContentType = "application/json"; // レスポンスのContent-TypeをJSONに設定
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // ステータスコードを500に設定

            // クライアントにエラーレスポンスを送信
            return context.Response.WriteAsync(response);
        }
    }
}
