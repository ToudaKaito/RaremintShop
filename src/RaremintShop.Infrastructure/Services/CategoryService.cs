using RaremintShop.Core.Interfaces.Repositories;
using RaremintShop.Core.Interfaces.Services;
using RaremintShop.Core.Models;
using RaremintShop.Shared.Exceptions;
using static RaremintShop.Shared.Constants;


namespace RaremintShop.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        /// <summary>
        /// 全カテゴリ取得処理
        /// </summary>
        /// <returns>カテゴリのリストor空のリスト</returns>
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            // 全カテゴリ取得
            var categories = await _categoryRepository.GetAllAsync();

            // カテゴリが存在する場合はリストとして返し、存在しない場合は空のリストを返す
            return categories.Any() ? categories.ToList() : [];
        }

        /// <summary>
        /// カテゴリ登録処理
        /// </summary>
        /// <param name="category">登録するカテゴリ情報</param>
        /// <returns>登録が成功した場合は true、それ以外は false</returns>
        /// <exception cref="BusinessException">
        /// - 同じ名前のカテゴリが既に存在する場合にスロー
        /// </exception>
        public async Task<bool> RegisterCategoryAsync(Category category)
        {
            // 引数NULLチェック
            ArgumentNullException.ThrowIfNull(category, nameof(category));

            // カテゴリ名の重複チェック
            if (await CategoryNameExistsAsync(category.Name))
            {
                throw new BusinessException(ErrorMessages.DuplicateName);
            }

            // カテゴリ登録処理
            return await _categoryRepository.AddAsync(category);
        }

        /// <summary>
        /// カテゴリ名の重複チェック処理
        /// </summary>
        /// <param name="categoryName">確認するカテゴリ名</param>
        /// <param name="excludeId">除外するカテゴリID（デフォルトは null）</param>
        /// <returns>カテゴリ名が存在する場合は true、それ以外は false</returns>
        public async Task<bool> CategoryNameExistsAsync(string categoryName, int? excludeId = null)
        {
            // 引数NULLチェック
            ArgumentNullException.ThrowIfNull(categoryName, nameof(categoryName));

            // 全カテゴリを取得してカテゴリ名の一致を確認
            // 大文字・小文字は区別しない
            var categories = await _categoryRepository.GetAllAsync();

            // カテゴリ名が一致する（大文字・小文字を区別しない）
            // 除外するカテゴリIDが指定されていない、または現在のカテゴリIDが除外IDと異なる
            return categories.Any(c =>
                c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase) &&
                (!excludeId.HasValue || c.Id != excludeId.Value));
        }

        /// <summary>
        /// 指定されたカテゴリIDに基づいてカテゴリ情報を取得します。
        /// </summary>
        /// <param name="id">取得するカテゴリのID</param>
        /// <returns>カテゴリ情報(該当するカテゴリが存在しない場合は例外をスロー)</returns>
        /// <exception cref="BusinessException">
        /// - ID が無効(0 以下)の場合にスローされます。
        /// - 指定されたカテゴリが存在しない場合にスローされます。
        /// </exception>
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            // 基本的にここを通ることはないが、念のため引数チェック
            if (id <= 0)
            {
                throw new BusinessException(ErrorMessages.InvalidIdError);
            }

            // カテゴリを取得
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new BusinessException(ErrorMessages.NotFoundError);
            }

            return category;
        }

        /// <summary>
        /// カテゴリ更新処理
        /// </summary>
        /// <param name="category">更新するカテゴリ情報</param>
        /// <returns>更新が成功した場合は true、それ以外は false</returns>
        /// <exception cref="BusinessException">
        /// - 指定されたカテゴリが存在しない場合にスロー
        /// - 同じ名前のカテゴリが既に存在する場合にスロー
        /// </exception>
        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            // 引数NULLチェック
            ArgumentNullException.ThrowIfNull(category, nameof(category));

            // 更新対象のカテゴリを取得
            var existingCategory = await _categoryRepository.GetByIdAsync(category.Id);
            if (existingCategory == null)
            {
                throw new BusinessException(ErrorMessages.NotFoundError);
            }

            // カテゴリ名の重複チェック
            if (await CategoryNameExistsAsync(category.Name, category.Id))
            {
                throw new BusinessException(ErrorMessages.DuplicateName);
            }

            // 更新対象のカテゴリ情報を変更
            existingCategory.Name = category.Name;
            existingCategory.UpdatedAt = DateTime.UtcNow;

            // 更新処理を実行
            return await _categoryRepository.UpdateAsync(existingCategory);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            // 基本的にここを通ることはないが、念のため引数チェック
            if (id <= 0)
            {
                throw new BusinessException(ErrorMessages.InvalidIdError);
            }

            // 削除対象のカテゴリを取得
            var deleteCategory = await _categoryRepository.GetByIdAsync(id);
            if (deleteCategory == null)
            {
                throw new BusinessException(ErrorMessages.NotFoundError);
            }

            // 削除処理を実行
            return await _categoryRepository.DeleteAsync(deleteCategory);
        }
    }
}
