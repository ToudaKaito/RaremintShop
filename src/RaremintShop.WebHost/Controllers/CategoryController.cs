using Microsoft.AspNetCore.Mvc;
using RaremintShop.Core.DTOs;
using RaremintShop.Core.Interfaces.Services;
using RaremintShop.Shared.Exceptions;
using RaremintShop.WebHost.Models;
using static RaremintShop.Shared.Constants;

namespace RaremintShop.WebHost.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        /// <summary>ロガー</summary>
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF 対策
        public async Task<IActionResult> Register(CategoryRegisterViewModel model)
        {
            // <バリデーション処理>
            if (!ModelState.IsValid)
            {
                // モデルが無効な場合、再度ビューを表示
                return View(model);
            }

            // <DTOへの変換>
            var dto = new CategoryDto
            {
                Name = model.Name
            };

            try
            {
                await _categoryService.RegisterCategoryAsync(dto);

                // 登録成功時、カテゴリ管理ページにリダイレクト
                return RedirectToAction(RedirectPaths.AdminCategoryManagement, RedirectPaths.AdminController);
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message); // ユーザー向けのエラーメッセージを設定
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);

                // ViewModelに変換
                var categoryViewModel = new CategoryEditViewModel
                {
                    Id = category.Id,
                    Name = category.Name
                };

                return View(category);
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message); // ユーザー向けのエラーメッセージを設定
                // 管理画面に戻す
                return RedirectToAction(RedirectPaths.AdminCategoryManagement, RedirectPaths.AdminController);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF 対策
        public async Task<IActionResult> Edit(CategoryEditViewModel model)
        {
            // <バリデーション処理>
            if (!ModelState.IsValid)
            {
                // モデルが無効な場合、再度ビューを表示
                return View(model);
            }

            // <DTOへの変換> 
            var categoryDto = new CategoryDto
            {
                Id = model.Id,
                Name = model.Name
            };

            try
            {
                // カテゴリ更新処理を実行
                await _categoryService.UpdateCategoryAsync(categoryDto);

                // 登録成功時、カテゴリ管理ページにリダイレクト
                return RedirectToAction(RedirectPaths.AdminCategoryManagement, RedirectPaths.AdminController);
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message); // ユーザー向けのエラーメッセージを設定
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF 対策
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // カテゴリ削除処理を実行
                await _categoryService.DeleteCategoryAsync(id);

                // 成功メッセージをTempDataに設定
                TempData["SuccessMessage"] = ErrorMessages.DeleteSuccess;
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                TempData["ErrorMessage"] = ex.Message; // ユーザー向けのエラーメッセージを設定
            }

            // カテゴリ管理ページにリダイレクト
            return RedirectToAction(RedirectPaths.AdminCategoryManagement, RedirectPaths.AdminController);
        }
    }
}