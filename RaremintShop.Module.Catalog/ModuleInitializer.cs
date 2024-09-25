using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RaremintShop.Module.Catalog.Data;
using RaremintShop.Module.Catalog.Services;
using RaremintShop.Module.Core;

namespace RaremintShop.Module.Catalog
{
    /// <summary>
    /// カタログモジュールの初期化処理を行うクラス。
    /// モジュール固有のサービスやデータベースの設定を行う。
    /// </summary>
    public class ModuleInitializer : IModuleInitializer
    {
        /// <summary>
        /// カタログモジュールに必要なサービスを登録するメソッド。
        /// データベースコンテキストやサービスの依存性注入を設定する。
        /// </summary>
        /// <param name="services">DIコンテナにサービスを登録するための IServiceCollection</param>
        /// <param name="configuration">アプリケーションの設定情報</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Catalogモジュール用のデータベースコンテキストを登録し、MySQLを使用
            services.AddDbContext<CatalogDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"), // WebHostの接続文字列を利用
                    new MySqlServerVersion(new Version(8, 0, 36))));

            // Catalogモジュールのサービスを依存性注入コンテナに登録
            services.AddScoped<IProductService, ProductService>();
        }

        /// <summary>
        /// カタログモジュールのルーティングやミドルウェアの設定を行うメソッド。
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
