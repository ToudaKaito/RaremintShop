namespace RaremintShop.Shared.Exceptions
{
    /// <summary>
    /// 業務例外を表すカスタム例外クラス
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException() : base() { }

        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, Exception innerException) : base(message, innerException) { }
    }
}