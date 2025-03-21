namespace RaremintShop.Shared
{
    public static class Constants
    {
        public static class  ErrorMessages
        {
            public const string UnexpectedError = "予期しないエラーが発生しました。";
            public const string InvalidLogin = "メールアドレスまたはパスワードが正しくありません。";
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
            public const string AdminDashboard = "Dashboard";
            public const string AdminUserManagement = "UserManagement";
            public const string AdminController = "Admin";

        }
    }
}
