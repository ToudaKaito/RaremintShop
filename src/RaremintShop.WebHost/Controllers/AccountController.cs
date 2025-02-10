using Microsoft.AspNetCore.Mvc;
using RaremintShop.Module.Identity.Models;
using RaremintShop.Module.Identity.Services;

namespace RaremintShop.WebHost.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        // コンストラクタ
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // 新規会員登録ページの表示
        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserRegisterViewModel());
        }

        // 新規会員登録フォームからのPOST処理
        [HttpPost]
        [ValidateAntiForgeryToken] // CSRFトークンの検証
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // ユーザー登録処理を非同期で呼び出し
            var result = await _userService.RegisterUserAsync(model);

            if (result.Succeeded)
            {
                // 登録成功: カタログページにリダイレクト
                return RedirectToAction("Index", "Catalog");
            }
            else
            {
                // 登録失敗: エラーメッセージをModelStateに追加
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
        }

        // ログインページの表示
        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserLoginViewModel());
        }

        // ログイン処理
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userService.LoginAsync(model);

            if (result.Succeeded)
            {
                var user = await _userService.GetByEmailAsync(model.Email);
                var roles = await _userService.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("DashBoard", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Catalog");
                }
            }
            else
            {
                // 登録失敗: エラーメッセージをModelStateに追加
                ModelState.AddModelError(string.Empty, "メールアドレスまたはパスワードが正しくありません。");

                return View(model);
            }
        }

        // ログアウト処理
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction("Index", "Catalog");
        }

        // 全てのユーザーを取得
        [HttpGet]
        public async Task<IActionResult> UsersList()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

    }
}
