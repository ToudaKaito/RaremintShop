using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaremintShop.Module.Identity.Models;
using RaremintShop.Module.Identity.Services;

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
            if (!ModelState.IsValid)
            {
                var roles = await _userService.GetAllRolesAsync();
                model.AvailableRoles = roles.Select(x => x.Name).ToList();
            }
            else
            {
                var result = await _userService.UpdateUserAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserManagement");
                }
            }
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
