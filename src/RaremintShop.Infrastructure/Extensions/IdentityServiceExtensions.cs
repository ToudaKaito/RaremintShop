using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RaremintShop.Core.Interfaces.Services;
using RaremintShop.Infrastructure.Data;
using RaremintShop.Infrastructure.Services;
using static RaremintShop.Shared.Constants;

namespace RaremintShop.Infrastructure.Extensions
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

            // UserServiceの追加（Identityに密接に関係するためここで登録）
            services.AddScoped<IUserService, UserService>();

            // Cookieおよびセッションの設定
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.Events.OnValidatePrincipal = async context =>
                {
                    var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
                    var user = await userManager.GetUserAsync(context.Principal!);
                    if (user != null)
                    {
                        var isAdmin = await userManager.IsInRoleAsync(user, Roles.Admin);

                        if (isAdmin)
                        {
                            options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                            options.SlidingExpiration = false;
                        }
                        else
                        {
                            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                            options.SlidingExpiration = true;
                        }
                    }
                };
            });

            return services;
        }
    }
}