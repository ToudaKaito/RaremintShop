using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RaremintShop.Core.Interfaces.Repositories;
using RaremintShop.Core.Interfaces.Services;
using RaremintShop.Infrastructure.Data;
using RaremintShop.Infrastructure.Extensions;
using RaremintShop.Infrastructure.Repositories;
using RaremintShop.Infrastructure.Services;
using RaremintShop.Shared;
using RaremintShop.Shared.Services;
using RaremintShop.WebHost.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// DbContextの登録（接続文字列はappsettings.jsonから取得）
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// サービスコンテナにコントローラーとビューを追加
builder.Services.AddControllersWithViews();

// ロギングサービスを追加
builder.Services.AddLogging();

// Identity/ユーザーサービスの登録（Userのエクステンション）
builder.Services.AddCustomIdentity();

// リポジトリ・サービスの登録（必要に応じて個別に記述）
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// ファイルストレージサービスの登録
// IWebHostEnvironmentを使用しているのでWebHostで追加する必要がある
builder.Services.AddSingleton<IFileStorageService>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var rootPath = Path.Combine(env.WebRootPath, "images");
    return new LocalFileStorageService(rootPath);
});

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

// --- ロールの初期化処理を追加 ---
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { Constants.Roles.User, Constants.Roles.Admin };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// アプリケーションを実行
app.Run();