namespace EventAPI.Exceptions
{
    public class DuplicateEntityException<T> : ApplicationException
    {
        public DuplicateEntityException(string message) : base(typeof(T).FullName + "： " + message)
        {
        }
    }
}