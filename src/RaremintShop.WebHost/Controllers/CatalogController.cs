using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaremintShop.Module.Catalog.Services;
using RaremintShop.Module.Catalog.Models;
using static RaremintShop.Shared.Constants;
using RaremintShop.Module.Identity.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using RaremintShop.Shared.Exceptions;

namespace RaremintShop.WebHost.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        /// <summary>ロガー</summary>
        private readonly ILogger<AdminController> _logger;

        public CatalogController(IProductService productService, ICategoryService categoryService, ILogger<AdminController> logger)
        {
            _productService = productService;
            _categoryService = categoryService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                //var products = await _productService.GetAllProductsAsync();
                return View(/*products*/);
            }
            catch (Exception ex)
            {
                // エラーメッセージを表示
                _logger.LogError(ex, ErrorMessages.ProductFetchError);
                ModelState.AddModelError(string.Empty, ErrorMessages.ProductFetchError);
                return View(new List<CatalogViewModel>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            var viewModel = new ProductRegisterViewModel
            {
                CategoryList = new SelectList(categories, "Id", "Name")
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ProductRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // カテゴリの取得
                var categories = await _categoryService.GetAllCategoriesAsync();
                model.CategoryList = new SelectList(categories, "Id", "Name");
                return View(model);
            }

            try
            {
                // 商品登録処理を実行
                var result = await _productService.RegisterProductAsync(model);

                if (result)
                {
                    // 登録成功時、商品管理ページにリダイレクト
                    TempData["SuccessMessage"] = ErrorMessages.RegisterSuccess;
                    return RedirectToAction(RedirectPaths.AdminProductManagement, RedirectPaths.AdminController);
                }
                else
                {
                    // 登録失敗時、エラーメッセージを表示
                    ModelState.AddModelError(string.Empty, ErrorMessages.RegisterError);
                    return View(model);
                }
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message); // ユーザー向けのエラーメッセージを設定
                return View(model);
            }
        }
    }
}
