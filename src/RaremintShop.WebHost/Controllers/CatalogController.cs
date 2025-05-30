using Microsoft.AspNetCore.Mvc;
using static RaremintShop.Shared.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using RaremintShop.Shared.Exceptions;
using RaremintShop.WebHost.Models;
using RaremintShop.Core.DTOs;
using RaremintShop.Core.Interfaces.Services;

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

            // <DTOへの変換>
            var dto = new ProductDto
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Stock = model.Stock,
                IsPublished = model.IsPublished
                // 画像は別で渡す
            };

            // <画像データの変換>
            var imageDatas = model.Images?.Select(file => new ProductImageData
            {
                FileName = file.FileName,
                Data = GetBytesFromFormFile(file)
            }).ToList() ?? new List<ProductImageData>();

            try
            {
                // 商品登録処理を実行
                await _productService.RegisterProductAsync(dto, imageDatas);

                // 登録成功時、商品管理ページにリダイレクト
                TempData["SuccessMessage"] = ErrorMessages.RegisterSuccess;
                return RedirectToAction(RedirectPaths.AdminProductManagement, RedirectPaths.AdminController);
            }
            catch (BusinessException ex)
            {
                // 業務例外をキャッチして処理
                _logger.LogWarning(ex, "{BusinessException} ExceptionMessage: {ExceptionMessage}", ErrorMessages.BusinessException, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);

                // カテゴリリスト再設定
                var categories = await _categoryService.GetAllCategoriesAsync();
                model.CategoryList = new SelectList(categories, "Id", "Name");
                return View(model);
            }
        }

        // IFormFileをbyte[]に変換するヘルパーメソッド
        private static byte[] GetBytesFromFormFile(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
