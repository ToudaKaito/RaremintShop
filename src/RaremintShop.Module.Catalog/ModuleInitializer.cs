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
    /// Catalogモジュールの初期化処理を行うクラス。
    /// </summary>
    public class ModuleInitializer : IModuleInitializer
    {
        /// <summary>
        /// Catalogモジュールのサービスを設定します。
        /// </summary>
        /// <param name="services">IServiceCollection インスタンス。依存性注入に使用される。</param>
        /// <param name="configuration">IConfiguration インスタンス。アプリケーションの設定情報を提供する。</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // CatalogDbContextの登録
            services.AddDbContext<CatalogDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

            // Catalogモジュールのサービスを依存性注入コンテナに登録
            services.AddScoped<IProductService, ProductService>();
        }

        /// <summary>
        /// Catalogモジュールのミドルウェアやルーティングを設定します。
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
