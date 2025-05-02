using Microsoft.AspNetCore.Mvc;
using RaremintShop.Module.Catalog.Models;
using RaremintShop.Module.Catalog.Services;
using RaremintShop.Shared.Exceptions;
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
        public async Task<IActionResult> Register(Category category)
        {
            // <バリデーション処理>
            if (!ModelState.IsValid)
            {
                // モデルが無効な場合、再度ビューを表示
                return View(category);
            }

            try
            {
                // カテゴリ登録処理を実行
                var result = await _categoryService.RegisterCategoryAsync(category);
                if (result)
                {
                    // 登録成功時、カテゴリ管理ページにリダイレクト
                    return RedirectToAction(RedirectPaths.AdminCategoryManagement, RedirectPaths.AdminController);
                }
                else
                {
                    // 登録失敗時、エラーメッセージを表示
                    ModelState.AddModelError(string.Empty, ErrorMessages.RegisterError);
                    return View(category);
                }
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message); // ユーザー向けのエラーメッセージを設定
                return View(category);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
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
        public async Task<IActionResult> Edit(Category category)
        {
            // <バリデーション処理>
            if (!ModelState.IsValid)
            {
                // モデルが無効な場合、再度ビューを表示
                return View(category);
            }

            try
            {
                // カテゴリ更新処理を実行
                var result = await _categoryService.UpdateCategoryAsync(category);

                if (result)
                {
                    // 登録成功時、カテゴリ管理ページにリダイレクト
                    return RedirectToAction(RedirectPaths.AdminCategoryManagement, RedirectPaths.AdminController);
                }
                else
                {
                    // 登録失敗時、エラーメッセージを表示
                    ModelState.AddModelError(string.Empty, ErrorMessages.UpdateError);
                    return View(category);
                }
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message); // ユーザー向けのエラーメッセージを設定
                return View(category);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF 対策
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // カテゴリ削除処理を実行
                var result = await _categoryService.DeleteCategoryAsync(id);

                if (result)
                {
                    // 成功メッセージをTempDataに設定
                    TempData["SuccessMessage"] = ErrorMessages.DeleteSuccess;
                }
                else
                {
                    // 失敗メッセージをTempDataに設定
                    TempData["ErrorMessage"] = ErrorMessages.DeleteError;
                }
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