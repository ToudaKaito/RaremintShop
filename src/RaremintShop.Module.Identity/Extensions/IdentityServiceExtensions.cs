using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RaremintShop.Module.Identity.Data;

namespace RaremintShop.Module.Identity.Extensions
{
    /// <summary>
    /// Identityの設定を拡張するためのクラス。
    /// サービスコレクションにカスタムIdentityの設定を追加します。
    /// </summary>
    public static class IdentityServiceExtensions
    {
        /// <summary>
        /// Identityのカスタム設定を追加します。
        /// パスワードのポリシーやストレージの設定を行います。
        /// </summary>
        /// <param name="services">DIコンテナのサービスコレクション。</param>
        /// <returns>カスタムIdentityの設定が追加されたサービスコレクション。</returns>
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // パスワードの設定
                // パスワードに少なくとも1つの数字を要求
                // パスワードの最小長を8文字に設定
                // パスワードに特殊文字を含める必要はない
                // パスワードに少なくとも1つの大文字を含める必要がある
                // パスワードに少なくとも1つの小文字を含める必要がある
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
            // ApplicationDbContextを使ってIdentityのユーザーやロール情報を保存
            .AddEntityFrameworkStores<IdentityDbContext>()
            // トークンプロバイダーをデフォルトで提供
            .AddDefaultTokenProviders();

            // Cookieおよびセッションの設定
            services.ConfigureApplicationCookie(options =>
            {
                // ログイン後のセッションのタイムアウトとセキュリティ設定
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

                // ユーザーが認証（ログイン）されていない状態でアクセスしようとした場合のリダイレクト先
                options.LoginPath = "/Account/Login";
                // アクセス権がないユーザーが特定のページにアクセスしようとした場合のリダイレクト先
                options.AccessDeniedPath = "/Account/AccessDenied";

                // ユーザーと管理者でセッションを分ける
                options.Events.OnValidatePrincipal = async context =>
                {
                    var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
                    var user = await userManager.GetUserAsync(context.Principal);
                    var isAdmin = await userManager.IsInRoleAsync(user, "Admin");

                    if (isAdmin)
                    {
                        // 管理者のセッション設定
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                        options.SlidingExpiration = false;
                    }
                    else
                    {
                        // ユーザーのセッション設定
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                        options.SlidingExpiration = true;
                    }
                };
            });

            return services;
        }

        /// <summary>
        /// アプリケーション起動時にロールと初期管理者ユーザーを作成します。
        /// </summary>
        /// <param name="serviceProvider">サービスプロバイダー</param>
        /// <returns>タスク</returns>
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            // ロール管理を行うサービスの取得
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            // ユーザー管理を行うサービスの取得
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // 作成したいロールの名前を定義
            string[] roleNames = { "Admin", "User" };

            // それぞれのロールが存在するか確認し、存在しなければ新規作成
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName); // ロールが存在するか確認
                if (!roleExist)
                {
                    // ロールが存在しない場合、新規作成
                    await roleManager.CreateAsync(new IdentityRole(roleName)); // ロールの作成
                }
            }

            // 初期管理者ユーザーのメールアドレスとパスワードを設定
            var adminEmail = "admin@example.com";
            var adminPassword = "Admin123!";

            // 管理者ユーザーが存在するか確認
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                // 管理者ユーザーが存在しない場合、新規作成
                adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
                var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword); // 管理者ユーザーを作成

                if (createAdminUser.Succeeded)
                {
                    // 作成に成功したら、管理者ロールを追加
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
