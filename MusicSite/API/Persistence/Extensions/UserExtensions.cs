using Microsoft.EntityFrameworkCore;
using MusicSite.API.Persistence.Entities;

namespace MusicSite.API.Persistence.Extensions
{
    public static class UserExtensions
    {
        public static async Task<User> FindByEmailAsync(this IQueryable<User> query, string email, CancellationToken token)
        {
            email = email.ToLower();
            return await query.FirstOrDefaultAsync(x => x.Email.Equals(email), token);
        }
    }
}
