using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaremintShop.Module.Catalog.Services;
using RaremintShop.Module.Catalog.Models;
using static RaremintShop.Shared.Constants;
using RaremintShop.Module.Identity.Models;

namespace RaremintShop.WebHost.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;

        /// <summary>ロガー</summary>
        private readonly ILogger<AdminController> _logger;

        public CatalogController(IProductService productService, ILogger<AdminController> logger)
        {
            _productService = productService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return View(products);
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
        public IActionResult Register()
        {
            return View(new UserRegisterViewModel());
        }
    }
}
