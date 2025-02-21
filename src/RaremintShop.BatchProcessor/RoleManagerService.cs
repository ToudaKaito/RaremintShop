using Microsoft.AspNetCore.Identity;

namespace RaremintShop.BatchProcessor
{
    public class RoleManagerService
    {
        // 管理者情報
        private const string ADMIN_ADDRESS = "admin.666@gmail.com";
        private const string ADMIN_PASSWORD = "Admin!123456";

        // Manager
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        // ロール名
        private readonly string[] roleNames = ["Admin", "User"];


        // コンストラクタ
        public RoleManagerService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // ロールの初期化
        public async Task CreateRoleAsync()
        {
            foreach (var roleName in roleNames)
            {
                if (await _roleManager.FindByNameAsync(roleName) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        // 既存ユーザーへのロール付与
        public async Task AssignRolesToUsersAsync()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);

                // 現在のロールをすべて削除
                if (currentRoles.Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                }

                if (user.Email == ADMIN_ADDRESS) // 管理者
                {
                    if (!await _userManager.IsInRoleAsync(user, roleNames[0]))
                    {
                        await _userManager.AddToRoleAsync(user, roleNames[0]);
                    }
                }
                else // 一般ユーザー
                {
                    if (!await _userManager.IsInRoleAsync(user, roleNames[1]))
                    {
                        await _userManager.AddToRoleAsync(user, roleNames[1]);
                    }
                }
            }

        }
    }
}
