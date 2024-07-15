namespace TinyUrl.Exceptions
{
    [Serializable]
    internal class NotAValidUrlException : Exception
    {
        public NotAValidUrlException(string? message) : base(message)
        {
        }
    }
}