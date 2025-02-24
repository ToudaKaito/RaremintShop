using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaremintShop.Module.Identity.Models;
using RaremintShop.Module.Identity.Services;
using System.Diagnostics;

namespace RaremintShop.WebHost.Controllers
{
    [Authorize(Roles = "Admin")] // 管理者のみアクセス可
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult DashBoard()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserManagement()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userService.GetByIdForEditAsync(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            Console.WriteLine("Edit");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("バリデーション失敗");

                // ModelState のエラーメッセージをコンソールに出力
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"キー: {state.Key}, エラーメッセージ: {error.ErrorMessage}");
                    }
                }

                var roles = await _userService.GetAllRolesAsync();
                model.AvailableRoles = roles.Select(x => x.Name).ToList();
                return View(model); // エラーメッセージを表示するためにビューを再表示
            }

            try
            {
                Console.WriteLine("try");
                var result = await _userService.UpdateUserAsync(model);
                if (result.Succeeded)
                {
                    Console.WriteLine("成功");
                    return RedirectToAction("UserManagement");
                }
                else
                {
                    Console.WriteLine("失敗");
                    // 更新が失敗した場合のエラーメッセージを追加
                    ModelState.AddModelError(string.Empty, "ユーザーの更新に失敗しました。");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                // 例外が発生した場合のエラーメッセージを追加
                ModelState.AddModelError(string.Empty, $"エラーが発生しました: {ex.Message}");
            }

            // 失敗した場合、AvailableRolesを再設定してビューを再表示
            var allRoles = await _userService.GetAllRolesAsync();
            model.AvailableRoles = allRoles.Select(x => x.Name).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            await _userService.DeleteUserAsync(user);
            return RedirectToAction("UserManagement");
        }
    }
}
