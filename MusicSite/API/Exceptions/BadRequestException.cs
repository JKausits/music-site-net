namespace MusicSite.API.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(params string[] messages)
        {
            Messages = messages;
        }

        public string[] Messages { get; }
    }
}
