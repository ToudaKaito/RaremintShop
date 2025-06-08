using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RaremintShop.Core.Interfaces.Repositories;
using RaremintShop.Core.Interfaces.Services;
using RaremintShop.Infrastructure.Data;
using RaremintShop.Infrastructure.Extensions;
using RaremintShop.Infrastructure.Repositories;
using RaremintShop.Infrastructure.Services;
using RaremintShop.Shared;
using RaremintShop.Shared.Services;
using RaremintShop.WebHost.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// DbContext�̓o�^�i�ڑ��������appsettings.json����擾�j
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// �T�[�r�X�R���e�i�ɃR���g���[���[�ƃr���[��ǉ�
builder.Services.AddControllersWithViews();

// ���M���O�T�[�r�X��ǉ�
builder.Services.AddLogging();

// Identity/���[�U�[�T�[�r�X�̓o�^�iUser�̃G�N�X�e���V�����j
builder.Services.AddCustomIdentity();

// ���|�W�g���E�T�[�r�X�̓o�^�i�K�v�ɉ����ČʂɋL�q�j
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// �t�@�C���X�g���[�W�T�[�r�X�̓o�^
// IWebHostEnvironment���g�p���Ă���̂�WebHost�Œǉ�����K�v������
builder.Services.AddSingleton<IFileStorageService>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var rootPath = Path.Combine(env.WebRootPath, "images");
    return new LocalFileStorageService(rootPath);
});

var app = builder.Build();

// �{�Ԋ��p�̃G���[�n���h�����O
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// HTTPS�ւ̃��_�C���N�g��L���ɂ���~�h���E�F�A
app.UseHttpsRedirection();

// �ÓI�t�@�C���iCSS��摜�Ȃǁj��񋟂���~�h���E�F�A
app.UseStaticFiles();

// �Ǝ���O�����~�h���E�F�A
app.UseMiddleware<ExceptionHandlingMiddleware>();

// ���[�e�B���O��L���ɂ���~�h���E�F�A
app.UseRouting();

// �F�؃~�h���E�F�A��ǉ��i�K���F�~�h���E�F�A�̑O�ɒǉ�����K�v������܂��j
app.UseAuthentication();
app.UseAuthorization();

// �f�t�H���g���[�g�ݒ�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Index}/{id?}");

// --- ���[���̏�����������ǉ� ---
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { Constants.Roles.User, Constants.Roles.Admin };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// �A�v���P�[�V���������s
app.Run();