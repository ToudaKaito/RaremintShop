using Microsoft.AspNetCore.Hosting;
using RaremintShop.Shared.Services;
using static RaremintShop.Shared.Constants;

namespace RaremintShop.Infrastructure.Services
{
    /// <summary>
    /// ローカルストレージにファイルを保存するサービスクラス
    /// </summary>
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _rootPath;

        public LocalFileStorageService(IWebHostEnvironment environment)
        {
            if (environment == null)
                throw new ArgumentNullException(nameof(environment));

            // ルートパスを設定（例: wwwroot/images）
            _rootPath = Path.Combine(environment.WebRootPath, "images");
        }

        /// <summary>
        /// ファイルを指定カテゴリに保存し、保存後の相対パスを返す
        /// </summary>
        /// <param name="fileData">保存するファイルのバイナリデータ</param>
        /// <param name="fileName">元のファイル名（拡張子を含む）</param>
        /// <param name="category">保存先のカテゴリ（例: "products", "users"）</param>
        /// <returns>保存されたファイルの相対パス（例: /images/products/xxxx.jpg）</returns>
        public async Task<string> SaveFileAsync(byte[] fileData, string fileName, string category)
        {
            ArgumentNullException.ThrowIfNull(fileData);
            ArgumentNullException.ThrowIfNull(fileName);
            ArgumentNullException.ThrowIfNull(category);

            // ファイルが空の場合は例外
            if (fileData.Length == 0)
                throw new ArgumentException(ErrorMessages.FileEmptyError);

            // カテゴリが空白の場合は例外
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException(ErrorMessages.FileCategoryMissingError);

            // 保存先ディレクトリをカテゴリごとに分ける（例: wwwroot/images/products）
            var categoryPath = Path.Combine(_rootPath, category);

            // ディレクトリが存在しない場合は作成
            if (!Directory.Exists(categoryPath))
                Directory.CreateDirectory(categoryPath);

            // ファイルの拡張子を取得
            var extension = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
                throw new ArgumentException(ErrorMessages.FileExtensionMissingError, nameof(fileName));

            // 許可された拡張子かどうかをチェック
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            if (!allowedExtensions.Contains(extension.ToLower()))
                throw new ArgumentException(ErrorMessages.FileFormatNotAllowedError, nameof(fileName));

            // ファイル名をランダムに生成して上書き防止
            var newFileName = $"{Guid.NewGuid()}{extension}";

            // 実際の保存先パスを作成
            var filePath = Path.Combine(categoryPath, newFileName);

            // バイナリデータを指定したパスに非同期で書き込む
            await File.WriteAllBytesAsync(filePath, fileData);

            // 保存先のURLパスを生成（クライアントから参照できる形式に変換）
            var relativePath = Path.Combine("images", category, newFileName).Replace("\\", "/");

            // スラッシュで始めることで絶対パスっぽく見せる（例: /images/products/xxxx.jpg）
            return $"/{relativePath}";
        }
    }
}