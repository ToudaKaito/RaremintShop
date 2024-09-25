using RaremintShop.Infrastructure;
using RaremintShop.Module.Core;
using RaremintShop.Shared;

var builder = WebApplication.CreateBuilder(args);

// �T�[�r�X�R���e�i�ɃR���g���[���[�ƃr���[��ǉ�
builder.Services.AddControllersWithViews();

// Logger��DI�ݒ���s���A�A�v���P�[�V�����S�̂Ń��M���O���g�p�ł���悤�ɂ���
builder.Services.AddLogging();

// Infrastructure�v���W�F�N�g�ɂ���DI�ݒ��ǉ�
builder.Services.AddInfrastructure();

// --- ���W���[���̓o�^���� ---
/*
 * GlobalConfiguration���g�p���Ċe���W���[�����A�v���P�[�V�����ɓo�^�B
 * �����ł́ACore�AOrders�ACatalog�̊e���W���[����o�^���Ă���B
 * ���W���[���͂��ꂼ��ŗL�̏����������������Ƃ��ł���B
 */
GlobalConfiguration.RegisterModule("Core", typeof(RaremintShop.Module.Core.ModuleInitializer).Assembly);
GlobalConfiguration.RegisterModule("Orders", typeof(RaremintShop.Module.Orders.ModuleInitializer).Assembly);
GlobalConfiguration.RegisterModule("Catalog", typeof(RaremintShop.Module.Catalog.ModuleInitializer).Assembly);

// --- �o�^���ꂽ���W���[���̏����������𓮓I�Ɏ��s ---
/*
 * �e���W���[���ɂ����鏉�����������s�����߂ɁAIModuleInitializer�����������N���X��
 * ���W���[�����ƂɌ������A���̃N���X�̃C���X�^���X�𐶐����ď��������������s����B
 */
foreach (var module in GlobalConfiguration.Modules)
{
    // IModuleInitializer���������Ă���N���X��T��
    var moduleInitializerType = module.Assembly.GetTypes()
        .FirstOrDefault(t => typeof(IModuleInitializer).IsAssignableFrom(t) && t.IsClass);

    if (moduleInitializerType != null)
    {
        // IModuleInitializer���������Ă���N���X�̃C���X�^���X�𐶐�
        var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType);

        // �e���W���[�����Ƃ�DI��ݒ��o�^
        moduleInitializer.ConfigureServices(builder.Services, builder.Configuration);
    }
}


var app = builder.Build();

// --- HTTP���N�G�X�g�p�C�v���C���̐ݒ� ---
/*
 * �A�v���P�[�V�������J�����ȊO�i�{�Ԋ��Ȃǁj�̏ꍇ�A
 * �G���[�n���h�����O�p�̃~�h���E�F�A��ݒ肷��B
 */
if (!app.Environment.IsDevelopment())
{
    // �G���[�n���h���[��ݒ肵�A�G���[�������� /Home/Error �Ƀ��_�C���N�g
    app.UseExceptionHandler("/Home/Error");
    // HSTS���g�p���āAHTTP Strict Transport Security�iHSTS�j�v���g�R����L���ɂ���
    app.UseHsts();
}

// HTTPS�ւ̃��_�C���N�g��L���ɂ���~�h���E�F�A
app.UseHttpsRedirection();

// �ÓI�t�@�C���iCSS��摜�Ȃǁj��񋟂���~�h���E�F�A
app.UseStaticFiles();

// ���[�e�B���O��L���ɂ���~�h���E�F�A
app.UseRouting();

// �F�؁E�F�~�h���E�F�A�̒ǉ��i�K�v�ɉ����ĔF�؏������s���j
app.UseAuthorization();

// �f�t�H���g�̃��[�e�B���O�ݒ�
/*
 * �R���g���[�����A�A�N�V�������A�I�v�V������ID���܂ރp�^�[���Ɋ�Â���
 * ���[�e�B���O��ݒ肷��B
 * ��F/Home/Index�A/Home/Error/{id} �Ȃ�
 */
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// �A�v���P�[�V���������s
app.Run();
