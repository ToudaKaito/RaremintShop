using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RaremintShop.Module.Identity.Models;

namespace RaremintShop.Module.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // コンストラクタで UserManager を注入
        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
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
            var usersList = new List<UsersListViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();
                usersList.Add(new UsersListViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = role
                });
            }

            return usersList;
        }

        // メールアドレスからユーザーを取得
        public async Task<IdentityUser> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        // ID からユーザーを取得
        public async Task<IdentityUser> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        // IDからユーザーを取得(Edit用)
        public async Task<UserEditViewModel> GetByIdForEditAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            var model = new UserEditViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = role,
                AvailableRoles = roles.ToList(), // 明示的な変換を追加
                IsActive = user.LockoutEnd == null
            };
            return model;
        }

        // ユーザーのロールを取得
        public async Task<string> GetRolesAsync(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            return role;
        }

        // ユーザーの削除
        public async Task<IdentityResult> DeleteUserAsync(IdentityUser user)
        {
            return await _userManager.DeleteAsync(user);
        }

        // 全てのロールを取得
        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        // ユーザー―情報を更新
        public async Task<IdentityResult> UpdateUserAsync(UserEditViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            user.Email = model.Email;
            user.UserName = model.Email;
            
            // アカウントの有効・無効を設定
            user.LockoutEnd = model.IsActive? null : DateTimeOffset.MaxValue;

            // 既存のロールを削除し、新しいロールを設定
            var currentRoles = _userManager.GetRolesAsync(user).Result;
            _userManager.RemoveFromRolesAsync(user, currentRoles).Wait();
            _userManager.AddToRolesAsync(user, new List<string> { model.Role }).Wait();
            //　ユーザー情報を更新
            return await _userManager.UpdateAsync(user);
        }
    }
}
