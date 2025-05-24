using RaremintShop.Core.DTOs;
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
        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            // 全カテゴリ取得
            var categories = await _categoryRepository.GetAllAsync();

            // Entity → DTO へ変換
            var dtoList = categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToList();

            // カテゴリが存在する場合はリストとして返し、存在しない場合は空のリストを返す
            return dtoList;
        }

        /// <summary>
        /// カテゴリ登録処理
        /// </summary>
        /// <param name="category">登録するカテゴリ情報</param>
        /// <returns>登録が成功した場合は true、それ以外は false</returns>
        /// <exception cref="BusinessException">
        /// - 同じ名前のカテゴリが既に存在する場合にスロー
        /// </exception>
        public async Task RegisterCategoryAsync(CategoryDto categoryDto)
        {
            // 引数NULLチェック
            ArgumentNullException.ThrowIfNull(categoryDto, nameof(categoryDto));

            // カテゴリ名の重複チェック
            if (await CategoryNameExistsAsync(categoryDto.Name))
            {
                throw new BusinessException(ErrorMessages.DuplicateName);
            }

            // Entityへの変換
            var category = new Category
            {
                Name = categoryDto.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            // カテゴリ登録処理
            var result = await _categoryRepository.AddAsync(category);
            if (!result)
            {
                throw new BusinessException(ErrorMessages.CategoryRegisterError);
            }
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
        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            // 基本的にここを通ることはないが、念のため引数チェック
            if (id <= 0)
            {
                throw new BusinessException(ErrorMessages.InvalidIdError);
            }

            // カテゴリを取得
            var category = await _categoryRepository.GetByIdAsync(id)
                ?? throw new BusinessException(ErrorMessages.NotFoundError);

            // DTOに変換
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };

            return categoryDto;
        }

        /// <summary>
        /// カテゴリ更新処理
        /// </summary>
        /// <param name="Dto">更新するカテゴリ情報</param>
        /// <returns>更新が成功した場合は true、それ以外は false</returns>
        /// <exception cref="BusinessException">
        /// - 指定されたカテゴリが存在しない場合にスロー
        /// - 同じ名前のカテゴリが既に存在する場合にスロー
        /// </exception>
        public async Task UpdateCategoryAsync(CategoryDto Dto)
        {
            // 引数NULLチェック
            ArgumentNullException.ThrowIfNull(Dto, nameof(Dto));

            // 更新対象のカテゴリを取得
            var existingCategory = await _categoryRepository.GetByIdAsync(Dto.Id);
            if (existingCategory == null)
            {
                throw new BusinessException(ErrorMessages.CategoryNotFound);
            }

            // カテゴリ名の重複チェック
            if (await CategoryNameExistsAsync(Dto.Name, Dto.Id))
            {
                throw new BusinessException(ErrorMessages.DuplicateName);
            }

            // 更新対象のカテゴリ情報を変更
            existingCategory.Name = Dto.Name;
            existingCategory.UpdatedAt = DateTime.UtcNow;

            // 更新処理を実行
            var result =await _categoryRepository.UpdateAsync(existingCategory);
            if (!result)
            {
                throw new BusinessException(ErrorMessages.CategoryUpdateError);
            }
        }

        public async Task DeleteCategoryAsync(int id)
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
                throw new BusinessException(ErrorMessages.CategoryNotFound);
            }

            // 削除処理を実行
            var result = await _categoryRepository.DeleteAsync(deleteCategory);
            if (!result)
            {
                throw new BusinessException(ErrorMessages.CategoryDeleteError);
            }
        }
    }
}
