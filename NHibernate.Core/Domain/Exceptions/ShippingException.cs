namespace NHibernatePoc.Core.Domain.Exceptions
{
    public class ShippingException : Exception
    {
        public ShippingException(string message)
            : base(message)
        {
        }

        public ShippingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
