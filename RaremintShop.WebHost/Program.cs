using RaremintShop.Infrastructure;
using RaremintShop.Module.Core;
using RaremintShop.Shared;

var builder = WebApplication.CreateBuilder(args);

// サービスコンテナにコントローラーとビューを追加
builder.Services.AddControllersWithViews();

// LoggerのDI設定を行い、アプリケーション全体でロギングを使用できるようにする
builder.Services.AddLogging();

// InfrastructureプロジェクトにあるDI設定を追加
builder.Services.AddInfrastructure();

// --- モジュールの登録処理 ---
/*
 * GlobalConfigurationを使用して各モジュールをアプリケーションに登録。
 * ここでは、Core、Orders、Catalogの各モジュールを登録している。
 * モジュールはそれぞれ固有の初期化処理を持つことができる。
 */
GlobalConfiguration.RegisterModule("Core", typeof(RaremintShop.Module.Core.ModuleInitializer).Assembly);
GlobalConfiguration.RegisterModule("Orders", typeof(RaremintShop.Module.Orders.ModuleInitializer).Assembly);
GlobalConfiguration.RegisterModule("Catalog", typeof(RaremintShop.Module.Catalog.ModuleInitializer).Assembly);

// --- 登録されたモジュールの初期化処理を動的に実行 ---
/*
 * 各モジュールにおける初期化処理を行うために、IModuleInitializerを実装したクラスを
 * モジュールごとに検索し、そのクラスのインスタンスを生成して初期化処理を実行する。
 */
foreach (var module in GlobalConfiguration.Modules)
{
    // IModuleInitializerを実装しているクラスを探す
    var moduleInitializerType = module.Assembly.GetTypes()
        .FirstOrDefault(t => typeof(IModuleInitializer).IsAssignableFrom(t) && t.IsClass);

    if (moduleInitializerType != null)
    {
        // IModuleInitializerを実装しているクラスのインスタンスを生成
        var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType);

        // 各モジュールごとのDIや設定を登録
        moduleInitializer.ConfigureServices(builder.Services, builder.Configuration);
    }
}


var app = builder.Build();

// --- HTTPリクエストパイプラインの設定 ---
/*
 * アプリケーションが開発環境以外（本番環境など）の場合、
 * エラーハンドリング用のミドルウェアを設定する。
 */
if (!app.Environment.IsDevelopment())
{
    // エラーハンドラーを設定し、エラー発生時に /Home/Error にリダイレクト
    app.UseExceptionHandler("/Home/Error");
    // HSTSを使用して、HTTP Strict Transport Security（HSTS）プロトコルを有効にする
    app.UseHsts();
}

// HTTPSへのリダイレクトを有効にするミドルウェア
app.UseHttpsRedirection();

// 静的ファイル（CSSや画像など）を提供するミドルウェア
app.UseStaticFiles();

// ルーティングを有効にするミドルウェア
app.UseRouting();

// 認証・認可ミドルウェアの追加（必要に応じて認証処理を行う）
app.UseAuthorization();

// デフォルトのルーティング設定
/*
 * コントローラ名、アクション名、オプションでIDを含むパターンに基づいて
 * ルーティングを設定する。
 * 例：/Home/Index、/Home/Error/{id} など
 */
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// アプリケーションを実行
app.Run();
