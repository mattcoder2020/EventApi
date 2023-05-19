namespace EventAPI.Exceptions
{
    /// <summary>
    /// Used in services to indicate that a resource was not found
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NotFoundException<T> : ApplicationException
    {
        public NotFoundException(string message) : base(typeof(T).FullName + "： " + message)
        {
        }
    }

    public class DuplicateEntityException<T> : ApplicationException
    {
        public DuplicateEntityException(string message) : base(typeof(T).FullName + "： " + message)
        {
        }
    }

    public class InvalidAddOperation<T> : ApplicationException
    {
        public InvalidAddOperation(string message) : base(typeof(T).FullName + "： " + message)
        {
        }
    }
}