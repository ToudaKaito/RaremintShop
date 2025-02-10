using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RaremintShop.Module.Identity.Data;
using RaremintShop.BatchProcessor;

class Program
{
    static async Task Main(string[] args)
    {
        // ServiceCollectionの作成
        var services = new ServiceCollection();

        // Configurationのセットアップ
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())  // カレントディレクトリを設定
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)  // appsettings.jsonを追加
            .Build();


        // サービスの構成
        ConfigureServices(services, configuration);

        // ServiceProviderの作成
        var serviceProvider = services.BuildServiceProvider();

        // ログのセットアップ
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Starting application");

        try
        {
            // アプリケーションの実行
            await RunApplicationAsync(serviceProvider);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred.");
        }

        logger.LogInformation("Application finished");
    }


    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // ログのセットアップ
        services.AddLogging(configure => configure.AddConsole());

        // IdentityDbContextの登録
        services.AddDbContext<IdentityDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

        // Identity関連サービスの登録
        services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>();

        // RoleManagerServiceの登録
        services.AddTransient<RoleManagerService>();
    }

    private static async Task RunApplicationAsync(IServiceProvider serviceProvider)
    {
        // RoleManagerServiceの取得
        var roleManagerService = serviceProvider.GetRequiredService<RoleManagerService>();

        // ロールの作成
        await roleManagerService.CreateRoleAsync();

        // ユーザーへのロール付与
        await roleManagerService.AssignRolesToUsersAsync();
    }
}