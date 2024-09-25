using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;


namespace RaremintShop.Module.Core
{
    /// <summary>
    /// モジュールの初期化処理を定義するインターフェース。
    /// 各モジュールが必要なサービスの登録やミドルウェアの設定を行うために使用される。
    /// </summary>
    public interface IModuleInitializer
    {
        /// <summary>
        /// モジュールのサービスを設定するメソッド。
        /// モジュール固有のサービスや依存関係をDIコンテナに登録するために使用される。
        /// </summary>
        /// <param name="services">IServiceCollection インスタンス。依存性注入に使用される。</param>
        /// <param name="configuration">IConfiguration インスタンス。アプリケーションの設定情報を提供する。</param>
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);

        /// <summary>
        /// モジュールのミドルウェアやルーティングを設定するメソッド。
        /// 必要に応じてミドルウェアやルーティングの設定を行う。
        /// </summary>
        /// <param name="app">IApplicationBuilder インスタンス。アプリケーションのリクエストパイプラインを構築するために使用される。</param>
        /// <param name="env">IWebHostEnvironment インスタンス。アプリケーションのホスティング環境を提供する。</param>
        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}
