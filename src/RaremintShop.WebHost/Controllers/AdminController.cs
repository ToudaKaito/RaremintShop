using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
