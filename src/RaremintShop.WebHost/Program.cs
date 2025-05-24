using RaremintShop.Infrastructure;
using RaremintShop.WebHost.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// サービスコンテナにコントローラーとビューを追加
builder.Services.AddControllersWithViews();

// ロギングサービスを追加
builder.Services.AddLogging();

// InfrastructureプロジェクトにあるDI設定を追加
builder.Services.AddInfrastructure();

var app = builder.Build();

// 本番環境用のエラーハンドリング
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// HTTPSへのリダイレクトを有効にするミドルウェア
app.UseHttpsRedirection();

// 静的ファイル（CSSや画像など）を提供するミドルウェア
app.UseStaticFiles();

// 独自例外処理ミドルウェア
app.UseMiddleware<ExceptionHandlingMiddleware>();

// ルーティングを有効にするミドルウェア
app.UseRouting();

// 認証ミドルウェアを追加（必ず認可ミドルウェアの前に追加する必要があります）
app.UseAuthentication();
app.UseAuthorization();

// デフォルトルート設定
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Index}/{id?}");

// アプリケーションを実行
app.Run();