namespace MusicSite.API.Common.Interfaces
{
    public interface IValidation<T> where T : class
    {
        Task<string> ValidateAsync(T entity, CancellationToken token);
    }
}
