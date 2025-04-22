namespace RaremintShop.Shared.Services
{
    public interface IFileStorageService
    {
        /// <summary>
        /// 指定カテゴリにファイルを保存して、保存パスを返す。
        /// </summary>
        /// <param name="fileData">ファイルのバイナリ</param>
        /// <param name="fileName">ファイル名</param>
        /// <param name="category">保存カテゴリ（例: "products", "users"）</param>
        /// <returns>保存されたパス（相対）</returns>
        Task<string> SaveFileAsync(byte[] fileData, string fileName, string category);
    }
}
