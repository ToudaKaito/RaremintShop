using System.Reflection;

namespace RaremintShop.Shared
{
    /// <summary>
    /// アプリケーションのグローバル設定を管理する静的クラス。
    /// </summary>
    public static class GlobalConfiguration
    {
        // モジュール情報のリスト
        public static List<ModuleInfo> Modules { get; } = new List<ModuleInfo>();

        /// <summary>
        /// 新しいモジュールを登録します。
        /// </summary>
        /// <param name="name">モジュールの名前。</param>
        /// <param name="assembly">モジュールのアセンブリ情報。</param>
        public static void RegisterModule(string name, Assembly assembly)
        {
            var moduleInfo = new ModuleInfo
            {
                Name = name,
                Assembly = assembly
            };
            Modules.Add(moduleInfo);
        }
    }

    /// <summary>
    /// モジュールの情報を保持するクラス。
    /// </summary>
    public class ModuleInfo
    {
        public required string Name { get; set; }
        public required Assembly Assembly { get; set; }
    }
}
