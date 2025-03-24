using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RaremintShop.Module.Core;
using RaremintShop.Module.Identity.Data;
using RaremintShop.Module.Identity.Extensions;
using RaremintShop.Module.Identity.Services;

namespace RaremintShop.Module.Identity
{
    /// <summary>
    /// Identityモジュールの初期化処理を行うクラス。
    /// </summary>
    public class ModuleInitializer : IModuleInitializer
    {
        /// <summary>
        /// Identityモジュールのサービスを設定します。
        /// </summary>
        /// <param name="services">IServiceCollection インスタンス。依存性注入に使用される。</param>
        /// <param name="configuration">IConfiguration インスタンス。アプリケーションの設定情報を提供する。</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // IdentityDbContextの登録
            // MySQLデータベースを使用してIdentityのユーザーやロール情報を保存
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

            // Identityのカスタム設定を追加
            services.AddCustomIdentity();

            // UserServiceの登録
            // IUserServiceインターフェースを実装したUserServiceクラスを依存性注入に登録
            services.AddScoped<IUserService, UserService>();
        }


        /// <summary>
        /// Identityモジュールのミドルウェアやルーティングを設定します。
        /// </summary>
        /// <param name="app">IApplicationBuilder インスタンス。アプリケーションのリクエストパイプラインを構築するために使用される。</param>
        /// <param name="env">IWebHostEnvironment インスタンス。アプリケーションのホスティング環境を提供する。</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 必要に応じてルーティングやミドルウェアの設定を行う
            // 現在は特に設定は行っていない
        }
    }
}