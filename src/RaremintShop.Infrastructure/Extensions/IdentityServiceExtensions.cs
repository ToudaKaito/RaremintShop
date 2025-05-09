using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RaremintShop.Infrastructure.Data;
using RaremintShop.Module.Identity.Services;
using static RaremintShop.Shared.Constants;

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
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // UserServiceの追加
            services.AddScoped<IUserService, UserService>();

            // Cookieおよびセッションの設定
            services.ConfigureApplicationCookie(options =>
            {
                // Cookieの設定
                options.Cookie.HttpOnly = true; // クッキーがJavaScriptからアクセスできないようにする
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // クッキーがHTTPS接続でのみ送信されるようにする

                // リダイレクトパスの設定
                options.LoginPath = "/Account/Login"; // 認証が必要なページにアクセスしようとしたときのリダイレクト先
                options.AccessDeniedPath = "/Account/AccessDenied"; // アクセス権がないページにアクセスしようとしたときのリダイレクト先

                // セッションの設定
                options.Events.OnValidatePrincipal = async context =>
                {
                    var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
                    var user = await userManager.GetUserAsync(context.Principal!);
                    if (user != null)
                    {
                        var isAdmin = await userManager.IsInRoleAsync(user, Roles.Admin);

                        if (isAdmin)
                        {
                            // 管理者のセッション設定
                            options.ExpireTimeSpan = TimeSpan.FromMinutes(15); // セッションの有効期限を15分に設定
                            options.SlidingExpiration = false; // スライディングエクスピレーションを無効にする
                        }
                        else
                        {
                            // 一般ユーザーのセッション設定
                            options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // セッションの有効期限を60分に設定
                            options.SlidingExpiration = true; // スライディングエクスピレーションを有効にする
                        }
                        // ※スライディングエクスピレーション: セッションの有効期限を延長する
                    }
                };
            });

            return services;
        }
    }
}