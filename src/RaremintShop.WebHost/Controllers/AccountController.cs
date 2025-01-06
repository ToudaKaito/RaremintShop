//using Microsoft.AspNetCore.Mvc;
//using RaremintShop.Module.Core.DTOs;
//using RaremintShop.Module.Identity.Services;

//namespace RaremintShop.WebHost.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly IUserService _userService;

//        //public AccountController(IUserService userService)
//        //{
//        //    _userService = userService;
//        //}

//        // ログインページの表示
//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }

//        // ログインフォームからのPOST処理
//        [HttpPost]
//        public IActionResult Login(UserLoginDto loginDto)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(loginDto);
//            }

//            //var user = _userService.Login(loginDto.Email, loginDto.Password);
//            //if (user != null)
//            //{
//            //    // ログイン成功: ホーム画面などにリダイレクト
//            //    return RedirectToAction("Index", "Catalog");
//            //}

//            //// ログイン失敗時のエラーメッセージ
//            //ModelState.AddModelError("", "メールアドレスまたはパスワードが間違っています");
//            return View(loginDto);
//        }

//        // 新規会員登録ページの表示
//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }

//        // 新規会員登録フォームからのPOST処理
//        [HttpPost]
//        public IActionResult Register(UserRegisterDto registerDto)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(registerDto);
//            }

//            //var result = _userService.Register(registerDto);
//            //if (result)
//            //{
//            //    // 登録成功: カタログページにリダイレクト
//            //    return RedirectToAction("Index", "Catalog");
//            //}

//            //// 登録失敗時のエラーメッセージ
//            //ModelState.AddModelError("", "ユーザー登録に失敗しました。再度お試しください。");
//            return View(registerDto);
//        }
//    }
//}
