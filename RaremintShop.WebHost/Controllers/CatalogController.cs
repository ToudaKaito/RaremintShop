using Microsoft.AspNetCore.Mvc;

namespace RaremintShop.WebHost.Controllers
{
    public class CatalogController : Controller
    {
        // 初期設定として、Indexアクションでビューを表示するだけのコード
        public IActionResult Index()
        {
            // とりあえずビューを返す
            return View();
        }
    }
}
