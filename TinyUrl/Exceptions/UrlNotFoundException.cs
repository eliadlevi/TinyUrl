namespace TinyUrl.Exceptions
{
    [Serializable]
    internal class UrlNotFoundException : Exception
    {
        public UrlNotFoundException(string? message) : base(message)
        {
        }
    }
}