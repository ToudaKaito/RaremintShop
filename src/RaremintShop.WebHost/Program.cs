using RaremintShop.Infrastructure;
using RaremintShop.WebHost.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// �T�[�r�X�R���e�i�ɃR���g���[���[�ƃr���[��ǉ�
builder.Services.AddControllersWithViews();

// ���M���O�T�[�r�X��ǉ�
builder.Services.AddLogging();

// Infrastructure�v���W�F�N�g�ɂ���DI�ݒ��ǉ�
builder.Services.AddInfrastructure();

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

// �A�v���P�[�V���������s
app.Run();