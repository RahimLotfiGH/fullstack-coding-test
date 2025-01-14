﻿using AUA.ProjectName.Services.ListService.Accounting.Contracts;
using AUA.ProjectName.Services.ListService.Accounting.Services;

namespace AUA.ProjectName.WebUi.RegistrationServices
{
    public static class ListService
    {

        public static void RegistrationListService(this IServiceCollection services)
        {
            services.RegistrationAccountingListService();
        }

        public static void RegistrationAccountingListService(this IServiceCollection services)
        {
            services.AddScoped<IAppUserListService, AppUserListService>();
            services.AddScoped<IRoleListService, RoleListService>();
            services.AddScoped<IUserAccessListService, UserAccessListService>();
            services.AddScoped<IUserRoleListService, UserRoleListService>();
            services.AddScoped<IUserRoleAccessListService, UserRoleAccessListService>();
        }
    }
}
