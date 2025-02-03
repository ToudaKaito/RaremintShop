using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RaremintShop.Module.Identity.Models;

namespace RaremintShop.Module.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        // コンストラクタで UserManager を注入
        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
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

        // ログイン
        public async Task<SignInResult> LoginAsync(UserLoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return SignInResult.Failed; // ユーザーが存在しない
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password,false,false);

            return result;
        }

        // ログアウト
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
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
