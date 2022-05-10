namespace MusicSite.API.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }

        public NotFoundException(Guid id, string name) : base($"{name} with ID '{id}' not found.")
        {

        }
    }
}
