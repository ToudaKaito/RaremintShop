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
    }
}
