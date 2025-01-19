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
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // IdentityDbContextの登録
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 36))));


            // Identityカスタムメソッドの呼び出し
            services.AddCustomIdentity();

            // UserServiceの登録
            services.AddScoped<IUserService, UserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 必要に応じてルーティングやミドルウェアの設定を行う
        }
    }
}
