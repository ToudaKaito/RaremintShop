namespace RaremintShop.Core.DTOs
{
    public class ProductImageData
    {
        public string FileName { get; set; } = string.Empty;
        public byte[] Data { get; set; } = Array.Empty<byte>();
    }
}
