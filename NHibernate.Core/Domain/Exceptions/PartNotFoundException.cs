namespace NHibernatePoc.Core.Domain.Exceptions
{
    public class PartNotFoundException : Exception
    {
        public PartNotFoundException(int partId)
            : base($"Part with ID {partId} not found.")
        {
        }
    }
}
