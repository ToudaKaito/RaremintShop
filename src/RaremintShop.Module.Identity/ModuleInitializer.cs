using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RaremintShop.Module.Core;
using RaremintShop.Module.Identity.Data;

namespace RaremintShop.Module.Identity
{
    /// <summary>
    /// Identityモジュールの初期化処理を行うクラス。
    /// モジュール固有のサービスやデータベースの設定を行う。
    /// </summary>
    public class ModuleInitializer : IModuleInitializer
    {
        /// <summary>
        /// Identityモジュールに必要なサービスを登録するメソッド。
        /// データベースコンテキストやサービスの依存性注入を設定する。
        /// </summary>
        /// <param name="services">DIコンテナにサービスを登録するための IServiceCollection</param>
        /// <param name="configuration">アプリケーションの設定情報</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Catalogモジュール用のデータベースコンテキストを登録し、MySQLを使用
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"), // WebHostの接続文字列を利用
                    new MySqlServerVersion(new Version(8, 0, 36))));

            // ASP.NET Core Identity の設定を行う
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();
        }

        /// <summary>
        /// Identityモジュールのルーティングやミドルウェアの設定を行うメソッド。
        /// 必要に応じて、アプリケーションに特定のミドルウェアやルーティングを追加する。
        /// </summary>
        /// <param name="app">アプリケーションの IApplicationBuilder</param>
        /// <param name="env">アプリケーションのホスティング環境</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 必要に応じてルーティングやミドルウェアの設定を行う
        }
    }
}
