using Microsoft.EntityFrameworkCore;
using MusicSite.API.Persistence;
using MusicSite.API.Persistence.Entities;

namespace MusicSite.API.Features.Shows.Validations
{
    public class ShowDatesNotConflictingValidation : ICreateShowValidation, IUpdateShowValidation
    {
        private readonly ApplicationDbContext _context;

        public ShowDatesNotConflictingValidation(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> ValidateAsync(Show entity, CancellationToken token)
        {
            var hasOverlappingDates = await _context.Shows
                .Where(x => !x.Id.Equals(entity.Id))
                .Where(x => x.StartAt < entity.EndAt && entity.StartAt < x.EndAt)
                .AnyAsync(token);

            if (hasOverlappingDates)
                return "This show's date/time conflicts with another show.";

            return null;
        }
    }
}
