using AUA.ProjectName.Services.ListService.Accounting.Contracts;
using AUA.ProjectName.Services.ListService.Accounting.Services;
using AUA.ProjectName.Services.ListService.Work.Contracts;
using AUA.ProjectName.Services.ListService.Work.Services;

namespace AUA.ProjectName.WebApi.RegistrationServices
{
    public static class ListService
    {

        public static void RegistrationListService(this IServiceCollection services)
        {
            services.RegistrationAccountingListService();
            services.RegistrationWorkListService();
        }

        public static void RegistrationAccountingListService(this IServiceCollection services)
        {
            services.AddScoped<IAppUserListService, AppUserListService>();
            services.AddScoped<IRoleListService, RoleListService>();
            services.AddScoped<IUserAccessListService, UserAccessListService>();
            services.AddScoped<IUserRoleListService, UserRoleListService>();
            services.AddScoped<IUserRoleAccessListService, UserRoleAccessListService>();
        }

        public static void RegistrationWorkListService(this IServiceCollection services)
        {
            services.AddScoped<IOrganizationListService, OrganizationListService>();
            services.AddScoped<IOrganizationStructureListService, OrganizationStructureListService>();
        }
    }
}
