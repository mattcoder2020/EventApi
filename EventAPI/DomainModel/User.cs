namespace EventAPI.DomainModel
{
    public class User:ModelBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}