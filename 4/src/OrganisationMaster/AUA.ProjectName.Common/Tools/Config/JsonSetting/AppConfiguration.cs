﻿using System.IO;
using AUA.ProjectName.Common.Consts;
using Microsoft.Extensions.Configuration;

namespace AUA.ProjectName.Common.Tools.Config.JsonSetting
{
    public static class AppConfiguration
    {
        public static string EfConnectionString => GetConfigurationRoot()
                                                   .GetSection(AppConsts.ConnectionStrings)
                                                   .GetSection(AppConsts.EfDbConnection)
                                                   .Value;

        public static string DapperConnectionString => GetConfigurationRoot()
                                                       .GetSection(AppConsts.ConnectionStrings)
                                                       .GetSection(AppConsts.DapperDbConnection)
                                                       .Value;

        public static string ApiUrlString => GetConfigurationRoot()
                                                   .GetSection(AppConsts.AppSetting)
                                                   .GetSection(AppConsts.BaseApiUrl)
                                                   .Value;

        public static string DefaultPassword => GetConfigurationRoot()
            .GetSection(AppConsts.AppSetting)
            .GetSection(AppConsts.DefaultPassword)
            .Value;

        public static IConfigurationRoot GetConfigurationRoot()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), AppConsts.AppSettingsFilePath);
            configurationBuilder.AddJsonFile(path, false);

            return configurationBuilder.Build();
        }

    }
}
