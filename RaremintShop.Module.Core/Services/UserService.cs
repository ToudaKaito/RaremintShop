using RaremintShop.Module.Core.Models;
using RaremintShop.Module.Core.Repositories;
using System.Threading.Tasks;

namespace RaremintShop.Module.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 非同期でユーザーIDに基づいてユーザーを取得します。
        /// </summary>
        /// <param name="userId">ユーザーのID</param>
        /// <returns>ユーザーエンティティ</returns>
        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }
            return user;
        }

        /// <summary>
        /// 非同期でメールアドレスに基づいてユーザーを取得します。
        /// </summary>
        /// <param name="email">ユーザーのメールアドレス</param>
        /// <returns>ユーザーエンティティ</returns>
        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                throw new Exception($"User with email {email} not found.");
            }
            return user;
        }

        /// <summary>
        /// 非同期で新しいユーザーを追加します。
        /// </summary>
        /// <param name="user">追加するユーザーエンティティ</param>
        public async Task AddUserAsync(User user)
        {
            // メールアドレスの重複チェック
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new Exception($"A user with email {user.Email} already exists.");
            }
            await _userRepository.AddUserAsync(user);
        }

        /// <summary>
        /// 非同期でユーザー情報を更新します。
        /// </summary>
        /// <param name="user">更新するユーザーエンティティ</param>
        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(user.UserID);
            if (existingUser == null)
            {
                throw new Exception($"User with ID {user.UserID} not found.");
            }
            await _userRepository.UpdateUserAsync(user);
        }
    }
}
