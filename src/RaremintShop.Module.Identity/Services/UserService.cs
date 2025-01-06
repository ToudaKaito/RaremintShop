using Microsoft.AspNetCore.Identity;

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

   

 
        
    }
}
