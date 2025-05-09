using Microsoft.Extensions.DependencyInjection;
using RaremintShop.Core.Interfaces.Repositories;
using RaremintShop.Infrastructure.Repositories;
using RaremintShop.Infrastructure.Services;
using RaremintShop.Shared.Services;

namespace RaremintShop.Infrastructure
{
    /// <summary>
    /// インフラストラクチャ層の依存性注入を提供する静的クラス。
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// サービスコレクションにインフラストラクチャ関連のサービスを追加します。
        /// </summary>
        /// <param name="services">サービスを追加する <see cref="IServiceCollection"/> オブジェクト。</param>
        /// <returns>サービスが追加された <see cref="IServiceCollection"/> オブジェクト。</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // リポジトリを登録
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // サービスを登録
            services.AddScoped<IFileStorageService, LocalFileStorageService>();

            return services;
        }
    }
}
