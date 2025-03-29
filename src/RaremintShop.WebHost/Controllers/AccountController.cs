using Microsoft.AspNetCore.Mvc;
using RaremintShop.Module.Identity.Models;
using RaremintShop.Module.Identity.Services;
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

            // <登録処理>
            try
            {
                // ユーザー登録処理を非同期で呼び出し
                var result = await _userService.RegisterUserAsync(model);

                if (result.Succeeded) // 登録成功:指定のページにダイレクト
                {
                    return RedirectToAction(RedirectPaths.CatalogIndex, RedirectPaths.CatalogController);
                }
                else // 登録失敗:エラーメッセージを設定して再度ビューを表示
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                // 例外が発生した場合の処理
                _logger.LogError(ex, ErrorMessages.UserRegisterError);
                ModelState.AddModelError(string.Empty, ErrorMessages.UserRegisterError);
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

            // <ログイン処理>
            try
            {
                // ユーザーログイン処理を非同期で呼び出し
                var result = await _userService.LoginAsync(model);

                if (result.Succeeded) // ログイン成功:ロールによってリダイレクト先を変更
                {
                    var user = await _userService.GetByEmailAsync(model.Email);
                    if (user == null) // 基本的にはありえないが、念のためのチェック
                    {
                        ModelState.AddModelError(string.Empty, ErrorMessages.UserNotFound);
                        return View(model);
                    }

                    var roles = await _userService.GetRolesAsync(user);

                    if (roles.Contains(Roles.Admin))
                    {
                        return RedirectToAction(RedirectPaths.AdminDashboard, RedirectPaths.AdminController);
                    }
                    else
                    {
                        return RedirectToAction(RedirectPaths.CatalogIndex, RedirectPaths.CatalogController);
                    }
                }
                else // ログイン失敗:エラーメッセージを設定して再度ビューを表示
                {
                    ModelState.AddModelError(string.Empty, ErrorMessages.InvalidLogin);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                // 例外が発生した場合の処理
                _logger.LogError(ex, ErrorMessages.UserLoginError);
                ModelState.AddModelError(string.Empty, ErrorMessages.UserLoginError);
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
