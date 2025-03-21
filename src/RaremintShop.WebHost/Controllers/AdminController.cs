using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaremintShop.Module.Identity.Models;
using RaremintShop.Module.Identity.Services;
using static RaremintShop.Shared.Constants;
using System.Diagnostics;

namespace RaremintShop.WebHost.Controllers
{
    [Authorize(Roles = Roles.Admin)] // 管理者のみアクセス可
    public class AdminController : Controller
    {
        /// <summary>ユーザー管理、認証、およびロール管理のためのサービス</summary>
        private readonly IUserService _userService;

        /// <summary>ロガー</summary>
        private readonly ILogger<AdminController> _logger;

        /// <summary>
        /// AdminController クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="userService">UserServiceのインターフェース</param>
        /// <param name="logger">ロギングのためのILogger</param>
        public AdminController(IUserService userService, ILogger<AdminController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        /// <summary>
        /// 管理者ダッシュボードページの表示
        /// </summary>
        /// <returns>管理者ダッシュボードページ</returns>
        public IActionResult DashBoard()
        {
            return View();
        }

        /// <summary>
        /// ユーザー管理ページの表示
        /// </summary>
        /// <returns>ユーザー管理ページ</returns>
        [HttpGet]
        public async Task<IActionResult> UserManagement()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return View(users);
            }
            catch (Exception ex)
            {
                // エラーメッセージを表示
                _logger.LogError(ex, "ユーザー情報の取得中にエラーが発生しました。");
                ModelState.AddModelError(string.Empty, "ユーザー情報の取得中にエラーが発生しました。");
                return View(new List<UserManagementViewModel>());
            }
        }

        /// <summary>
        /// ユーザー編集ページの表示
        /// </summary>
        /// <param name="id">ユーザーID</param>
        /// <returns>ユーザー編集ページ</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var user = await _userService.GetByIdForEditAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            catch (Exception ex)
            {
                // エラーメッセージを表示
                _logger.LogError(ex, "ユーザー情報の取得中にエラーが発生しました。");
                ModelState.AddModelError(string.Empty, "ユーザー情報の取得中にエラーが発生しました。");
                return RedirectToAction(RedirectPaths.AdminUserManagement, RedirectPaths.AdminController);
            }
        }



        /// <summary>
        /// ユーザー編集フォームからのPOST処理
        /// </summary>
        /// <param name="model">ユーザー編集のためのモデル</param>
        /// <returns>編集結果に応じたビュー</returns>
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
                model.AvailableRoles = roles.Select(x => x.Name!).ToList();
                return View(model);
            }

            try
            {
                Console.WriteLine("try");
                var result = await _userService.UpdateUserAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction(RedirectPaths.AdminUserManagement, RedirectPaths.AdminController);
                }
                else
                {
                    // エラーメッセージを表示
                    ModelState.AddModelError(string.Empty, "ユーザー情報の更新中にエラーが発生しました。");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                // ログの記録
                _logger.LogError(ex, "ユーザー情報の更新中にエラーが発生しました。");

                // エラーメッセージの表示
                ModelState.AddModelError(string.Empty, "予期しないエラーが発生しました。");
                return View(model);
            }

        }


        /// <summary>
        /// ユーザー削除処理
        /// </summary>
        /// <param name="id">ユーザーID</param>
        /// <returns>ユーザー管理ページにリダイレクト</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                await _userService.DeleteUserAsync(user);
                return RedirectToAction(RedirectPaths.AdminUserManagement, RedirectPaths.AdminController);
            }
            catch (Exception ex)
            {
                // ログの記録
                _logger.LogError(ex, "ユーザー削除中にエラーが発生しました。");

                // エラーメッセージの表示
                ModelState.AddModelError(string.Empty, "ユーザー削除中にエラーが発生しました。");
                return RedirectToAction("UserManagement");
            }
        }
    }
}
