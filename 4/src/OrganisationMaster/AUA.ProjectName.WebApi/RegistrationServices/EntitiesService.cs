using AUA.ProjectName.DataLayer.AppContext.EntityFrameworkContext;
using AUA.ProjectName.Services.EntitiesService.Accounting.Contracts;
using AUA.ProjectName.Services.EntitiesService.Accounting.Services;
using AUA.ProjectName.Services.EntitiesService.Work.Contracts;
using AUA.ProjectName.Services.EntitiesService.Work.Services;

namespace AUA.ProjectName.WebApi.RegistrationServices
{
    public static class EntitiesService
    {

        public static void RegistrationEntitiesService(this IServiceCollection services)
        {
            services.RegistrationBaseService();

            services.RegistrationAccountingService();

            services.RegistrationWorkService();

            services.RegistrationBaseInformationService();

        }

        private static void RegistrationBaseService(this IServiceCollection services)
        {
            services.AddDbContext<IUnitOfWork, ApplicationEfContext>();
        }

        private static void RegistrationAccountingService(this IServiceCollection services)
        {
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IUserAccessService, UserAccessService>();
            services.AddScoped<IUserRoleAccessService, UserRoleAccessService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IActiveAccessTokenService, ActiveAccessTokenService>();
            services.AddScoped<IRoleService, RoleService>();
        }

        private static void RegistrationWorkService(this IServiceCollection services)
        {
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IOrganizationStructureService, OrganizationStructureService>();
        }

        private static void RegistrationBaseInformationService(this IServiceCollection services)
        {
           
        }
    }
}
