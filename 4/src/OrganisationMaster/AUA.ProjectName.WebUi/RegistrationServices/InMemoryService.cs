using AUA.ProjectName.InMemoryServices.Contracts;
using AUA.ProjectName.InMemoryServices.Services;

namespace AUA.ProjectName.WebUi.RegistrationServices
{
    public static class InMemoryService
    {
        public static void RegistrationInMemoryService(this IServiceCollection services)
        {

            services.AddSingleton<IInMemoryUserAccessService>(new InMemoryUserAccessService());
            services.AddSingleton<IInMemoryLockedUsersService>(new InMemoryLockedUsersService());

        }
    }
}
