using Microsoft.AspNetCore.Mvc;
using RaremintShop.Module.Identity.Models;
using RaremintShop.Module.Identity.Services;

namespace RaremintShop.WebHost.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;


        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        // 新規会員登録ページの表示
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        // 新規会員登録フォームからのPOST処理
        [HttpPost]
        [ValidateAntiForgeryToken] // CSRFトークンの検証
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            Console.WriteLine("Registerメソッドスタート");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("残念");
                return View(model);
            }

            // ユーザー登録処理を非同期で呼び出し
            var result = await _userService.RegisterUserAsync(model);

            if (result.Succeeded)
            {
                Console.WriteLine("成功");
                // 登録成功: カタログページにリダイレクト
                return RedirectToAction("Index", "Catalog");
            }
            else
            {
                Console.WriteLine("失敗");
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
            return View();
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
