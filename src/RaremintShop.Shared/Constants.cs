namespace RaremintShop.Shared
{
    public static class Constants
    {
        public static class  ErrorMessages
        {
            public const string UnexpectedError = "予期しないエラーが発生しました。";
            public const string InvalidLogin = "メールアドレスまたはパスワードが正しくありません。";
            public const string UserNotFound = "ユーザーが見つかりません。";
            public const string UserFetchError = "ユーザー情報の取得中にエラーが発生しました。";
            public const string UserUpdateError = "ユーザー情報の更新中にエラーが発生しました。";
            public const string UserDeleteError = "ユーザー削除中にエラーが発生しました。";
            public const string UserRegisterError = "ユーザー登録中にエラーが発生しました。";
            public const string UserLoginError = "ユーザーログイン中にエラーが発生しました。";
            public const string UserLogoutError = "ユーザーログアウト中にエラーが発生しました。";
            public const string ProductFetchError = "商品の取得中にエラーが発生しました。";
            public const string CategoryFetchError = "カテゴリの取得中にエラーが発生しました。";
            public const string CategoryRegisterError = "カテゴリ登録中にエラーが発生しました。";
            public const string ProductRegisterError = "商品登録中にエラーが発生しました。";
        }

        public static class  Roles
        {
            public const string Admin = "Admin";
            public const string User = "User";
        }

        public static class RedirectPaths
        {
            public const string CatalogIndex = "Index";
            public const string CatalogController = "Catalog";
            public const string CategoryController = "Category";
            public const string AdminDashboard = "Dashboard";
            public const string AdminUserManagement = "UserManagement";
            public const string AdminController = "Admin";
            public const string AdminCategoryManagement = "CategoryManagement";
            public const string AdminProductManagement = "ProductManagement";
        }
    }
}
