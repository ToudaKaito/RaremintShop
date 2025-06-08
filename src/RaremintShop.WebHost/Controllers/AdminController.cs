using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaremintShop.Core.DTOs;
using RaremintShop.Core.Interfaces.Services;
using RaremintShop.Shared.Exceptions;
using RaremintShop.WebHost.Models;
using static RaremintShop.Shared.Constants;

namespace RaremintShop.WebHost.Controllers
{
    [Authorize(Roles = Roles.Admin)] // 管理者のみアクセス可
    public class AdminController : Controller
    {
        /// <summary>ユーザー管理、認証、およびロール管理のためのサービス</summary>
        private readonly IUserService _userService;

        /// <summary>商品管理のためのサービス</summary>
        private readonly IProductService _productService;

        /// <summary>カテゴリ管理のためのサービス</summary>
        private readonly ICategoryService _categoryService;

        /// <summary>ロガー</summary>
        private readonly ILogger<AdminController> _logger;

        /// <summary>
        /// AdminController クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="userService">UserServiceのインターフェース</param>
        /// <param name="logger">ロギングのためのILogger</param>
        public AdminController(IUserService userService, IProductService productService, ICategoryService categoryService, ILogger<AdminController> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
                var userManagementViewModels = new List<UserManagementViewModel>();
                var users = await _userService.GetAllUsersAsync();

                foreach(var user in users)
                {
                    userManagementViewModels.Add(new UserManagementViewModel
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        Role = user.Role,
                    });
                }
                return View(userManagementViewModels);
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message); // ユーザー向けのエラーメッセージを設定
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
                var user = await _userService.GetByIdAsync(id);
                var roles = await _userService.GetRolesAsync(user.Id);

                // UsrDto→UserEditViewModelに変換
                var userEditViewModel = new UserEditViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    Role = roles.FirstOrDefault() ?? string.Empty,
                    IsActive = user.IsActive,
                };
                return View(userEditViewModel);
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message); // ユーザー向けのエラーメッセージを設定
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
            // <バリデーション処理>
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // <DTOへの変換>
            var dto = new UserDto
            {
                Id = model.Id,
                UserName = model.UserName,
                Email = model.Email,
                Role = model.Role,
                IsActive = model.IsActive,
            };

            try
            {
                // <サービス呼び出し>
                // 基本的にはエラーの場合はcatchで処理する
                await _userService.UpdateUserAsync(dto);

                return RedirectToAction(RedirectPaths.AdminUserManagement, RedirectPaths.AdminController);
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message); // ユーザー向けのエラーメッセージを設定
                return RedirectToAction(RedirectPaths.AdminUserManagement, RedirectPaths.AdminController);
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
                // <サービス呼び出し>
                // 基本的にはエラーの場合はcatchで処理する
                await _userService.DeleteUserAsync(id);

                return RedirectToAction(RedirectPaths.AdminUserManagement, RedirectPaths.AdminController);
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message); // ユーザー向けのエラーメッセージを設定
                return RedirectToAction(RedirectPaths.AdminUserManagement, RedirectPaths.AdminController);
            }
        }

        // TODO
        [HttpGet]
        public async Task<IActionResult> ProductManagement()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                // DTO → ViewModel 変換
                var productManagementViewModels = products
                    .Select(p => new ProductManagementViewModel
                    {
                        Id = p.Id,
                        CategoryId = p.CategoryId,
                        Name = p.Name ?? string.Empty,
                        Description = p.Description ?? string.Empty,
                        Price = p.Price,
                        Stock = p.Stock,
                        IsPublished = p.IsPublished,
                        CreatedAt = p.CreatedAt,
                        UpdatedAt = p.UpdatedAt,
                        ImageUrls = p.ImageUrls ?? new List<string>()
                    })
                    .ToList();

                return View(productManagementViewModels);
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message); // ユーザー向けのエラーメッセージを設定
                return RedirectToAction(RedirectPaths.AdminDashboard, RedirectPaths.AdminController);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CategoryManagement()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();

                // ViewModelに変換
                var categoryManagementViewModels = categories
                    .Select(c => new CategoryManagementViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt
                    })
                    .ToList();
                return View(categoryManagementViewModels);
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message); // ユーザー向けのエラーメッセージを設定
                return RedirectToAction(RedirectPaths.AdminDashboard, RedirectPaths.AdminController);
            }
        }
    }
}
