namespace TinyUrl.Exceptions
{
    [Serializable]
    internal class DBConnectionException : Exception
    {
        public DBConnectionException()
        {
        }

        public DBConnectionException(string? message) : base(message)
        {
        }
    }
}