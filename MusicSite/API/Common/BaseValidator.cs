using MusicSite.API.Common.Interfaces;
using MusicSite.API.Exceptions;

namespace MusicSite.API.Common
{
    public class BaseValidator<T> where T : class
    {
        protected IEnumerable<IValidation<T>> Validations;

        public BaseValidator(IEnumerable<IValidation<T>> validations)
        {
            Validations = validations;
        }

        protected async Task RunValidationAsync<R>(T entity, CancellationToken token) where R : IValidation<T>
        {
            var validations = GetStrategies<R>();

            var errors = new List<string>();

            foreach (var validation in validations)
            {
                var error = await validation.ValidateAsync(entity, token);
                if (!string.IsNullOrEmpty(error))
                    errors.Add(error);
            }

            if (errors.Any())
                throw new BadRequestException(errors.ToArray());

        }


        #region Private
        private List<IValidation<T>> GetStrategies<R>() where R : IValidation<T>
        {
            var strategies = Validations.Where(x => x is R).ToList();

            return strategies;
        }
        #endregion
    }
}
