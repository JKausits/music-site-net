using MusicSite.API.Common;
using MusicSite.API.Common.Interfaces;
using MusicSite.API.Persistence.Entities;

namespace MusicSite.API.Features.Shows.Validations
{
    public interface IShowValidator
    {
        Task ValidateShowCreatedAsync(Show show, CancellationToken token);
        Task ValidateShowUpdatedAsync(Show show, CancellationToken token);
    }

    public class ShowValidator : BaseValidator<Show>, IShowValidator
    {
        public ShowValidator(IEnumerable<IValidation<Show>> validations) : base(validations)
        {
        }

        public async Task ValidateShowCreatedAsync(Show show, CancellationToken token)
        {
            await RunValidationAsync<ICreateShowValidation>(show, token);
        }

        public async Task ValidateShowUpdatedAsync(Show show, CancellationToken token)
        {
            await RunValidationAsync<IUpdateShowValidation>(show, token);
        }
    }
}
