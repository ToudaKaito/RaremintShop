namespace RaremintShop.Shared.Services
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(byte[] fileData, string fileName, string category);
    }
}
