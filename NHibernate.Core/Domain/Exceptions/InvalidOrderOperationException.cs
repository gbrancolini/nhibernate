namespace NHibernatePoc.Core.Domain.Exceptions
{
    public class InvalidOrderOperationException : Exception
    {
        public InvalidOrderOperationException(string message)
            : base(message)
        {
        }
    }
}
