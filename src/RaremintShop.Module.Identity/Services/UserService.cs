using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RaremintShop.Module.Identity.Models;

namespace RaremintShop.Module.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        // コンストラクタで UserManager を注入
        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // ユーザーの登録
        public async Task<IdentityResult> RegisterUserAsync(UserRegisterViewModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }

        // 全ユーザー取得
        public async Task<List<UsersListViewModel>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users.Select(user => new UsersListViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            }).ToList();
        }

    }
}
