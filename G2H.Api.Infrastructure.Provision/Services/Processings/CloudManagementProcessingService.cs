﻿// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using G2H.Api.Infrastructure.Provision.Brokers.Configurations;
using G2H.Api.Infrastructure.Provision.Models.Configurations;
using G2H.Api.Infrastructure.Provision.Models.Storages;
using G2H.Api.Infrastructure.Provision.Services.Foundations;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.Sql.Fluent;

namespace G2H.Api.Infrastructure.Provision.Services.Processings
{
    public class CloudManagementProcessingService : ICloudManagementProcessingService
    {
        private readonly ICloudManagementService cloudManagementService;
        private readonly IConfigurationBroker configurationBroker;

        public CloudManagementProcessingService()
        {
            this.cloudManagementService = new CloudManagementService();
            this.configurationBroker = new ConfigurationBroker();
        }

        public async ValueTask ProcessAsync()
        {
            CloudManagementConfiguration cloudManagementConfiguration =
                this.configurationBroker.GetConfigurations();

            await ProvisionAsync(
                projectName: cloudManagementConfiguration.ProjectName,
                cloudAction: cloudManagementConfiguration.Up);
        }

        private async ValueTask ProvisionAsync(
            string projectName,
            CloudAction cloudAction)
        {
            List<string> environments = RetrieveEnvironments(cloudAction);

            foreach (string environmentName in environments)
            {
                IResourceGroup resourceGroup = await this.cloudManagementService
                    .ProvisionResourceGroupAsync(
                        projectName,
                        environmentName);

                IAppServicePlan appServicePlan = await this.cloudManagementService
                    .ProvisionPlanAsync(
                        projectName,
                        environmentName,
                        resourceGroup);

                ISqlServer sqlServer = await this.cloudManagementService
                    .ProvisionSqlServerAsync(
                        projectName,
                        environmentName,
                        resourceGroup);

                SqlDatabase sqlDatabase = await this.cloudManagementService
                    .ProvisionSqlDatabaseAsync(
                        projectName,
                        environmentName,
                        sqlServer);

                IWebApp webApp = await this.cloudManagementService
                    .ProvisionWebAppAsync(
                        projectName,
                        environmentName,
                        sqlDatabase.ConnectionString,
                        resourceGroup,
                        appServicePlan);
            }
        }

        private static List<string> RetrieveEnvironments(CloudAction cloudAction) =>
            cloudAction?.Environments ?? new List<string>();
    }
}
