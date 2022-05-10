namespace MusicSite.API.Features.Shows.Validations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddShowValidation(this IServiceCollection services)
        {
            services.AddTransient<IShowValidation, ShowDatesNotConflictingValidation>();

            services.AddTransient<IShowValidator, ShowValidator>(opt => new ShowValidator(opt.GetServices<IShowValidation>()));
            return services;
        }
    }
}
