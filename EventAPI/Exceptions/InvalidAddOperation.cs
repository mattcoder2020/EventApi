namespace EventAPI.Exceptions
{
    public class InvalidAddOperationException<T> : ApplicationException
    {
        public InvalidAddOperationException(string message) : base(typeof(T).FullName + "： " + message)
        {
        }
    }
}