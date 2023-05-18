namespace EventAPI.Service
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
}