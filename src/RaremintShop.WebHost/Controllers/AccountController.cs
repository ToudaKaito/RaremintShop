using Microsoft.AspNetCore.Mvc;
using RaremintShop.Core.DTOs;
using RaremintShop.Module.Identity.Models;
using RaremintShop.Module.Identity.Services;
using RaremintShop.Shared.Exceptions;
using static RaremintShop.Shared.Constants;

namespace RaremintShop.WebHost.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>ユーザー管理、認証、およびロール管理のためのサービス</summary>
        private readonly IUserService _userService;

        /// <summary>ロガー</summary>
        private readonly ILogger<AccountController> _logger;


        /// <summary>
        /// AccountController クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="userService">UserServiceのインターフェース</param>
        /// <param name="logger">ロギングのためのILogger</param>
        public AccountController(IUserService userService, ILogger<AccountController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        /// <summary>
        /// 新規会員登録ページの表示
        /// </summary>
        /// <returns>新規会員登録ページ</returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserRegisterViewModel());
        }


        /// <summary>
        /// 新規会員登録フォームからのPOST処理
        /// </summary>
        /// <param name="model">ユーザー登録のためのモデル</param>
        /// <returns>登録結果に応じたビュー</returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // CSRFトークンの検証
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            // <バリデーション処理>
            if (!ModelState.IsValid)
            {
                // モデルが無効な場合、再度ビューを表示
                return View(model);
            }

            // <DTOへの変換>
            var dto = new UserRegisterDto
            {
                Email = model.Email,
                Password = model.Password
            };

            try
            {
                // <サービス呼び出し>
                // 基本的にはエラーの場合はcatchで処理する
                await _userService.RegisterUserAsync(dto);

                return RedirectToAction(RedirectPaths.CatalogIndex, RedirectPaths.CatalogController);
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message); // ユーザー向けのエラーメッセージを設定
                return View(model);
            }
        }


        /// <summary>
        /// ログインページの表示
        /// </summary>
        /// <returns>ログインページ</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserLoginViewModel());
        }


        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="model">ユーザーログインのためのモデル</param>
        /// <returns>ログイン結果に応じたビュー</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            // <バリデーション処理>
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // <DTOへの変換>
            var dto = new UserLoginDto
            {
                Email = model.Email,
                Password = model.Password
            };

            try
            {
                // <サービス呼び出し>
                // 基本的にはエラーの場合はcatchで処理する
                var userId = await _userService.LoginAsync(dto);


                var roles = await _userService.GetRolesAsync(userId);

                if (roles.Contains(Roles.Admin))
                {
                    return RedirectToAction(RedirectPaths.AdminDashboard, RedirectPaths.AdminController);
                }
                else
                {
                    return RedirectToAction(RedirectPaths.CatalogIndex, RedirectPaths.CatalogController);
                }
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message); // ユーザー向けのエラーメッセージを設定
                return View(model);
            }
        }


        /// <summary>
        /// ログアウト処理
        /// </summary>
        /// <returns>カタログページにリダイレクト</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _userService.LogoutAsync();
                return RedirectToAction(RedirectPaths.CatalogIndex, RedirectPaths.CatalogController);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.UserLogoutError);
                ModelState.AddModelError(string.Empty, ErrorMessages.UserLogoutError);
                return RedirectToAction(RedirectPaths.CatalogIndex, RedirectPaths.CatalogController);
            }
        }
    }
}
