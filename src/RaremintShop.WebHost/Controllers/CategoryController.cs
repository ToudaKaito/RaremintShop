using Microsoft.AspNetCore.Mvc;
using RaremintShop.Module.Catalog.Models;
using RaremintShop.Module.Catalog.Services;
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
            _categoryService = categoryService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
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
                var result = await _categoryService.RegisterCategoryAsync(category);
                if (result)
                {
                    // カテゴリの登録が成功した場合、カテゴリ管理ページにリダイレクト
                    return RedirectToAction(RedirectPaths.AdminCategoryManagement, RedirectPaths.AdminController);
                }
                else
                {
                    // TODO:失敗処理
                    return View(category);
                }
            }
            catch(Exception ex)
            {
                // 例外が発生した場合の処理
                _logger.LogError(ex, ErrorMessages.CategoryRegisterError);
                ModelState.AddModelError(string.Empty, ErrorMessages.CategoryRegisterError);
                return View(category);
            }
        }
    }
}
