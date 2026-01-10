targetScope = 'subscription'

@minLength(1)
@maxLength(64)
@description('Name of the environment that can be used as part of naming resource convention')
param environmentName string

@minLength(1)
@description('Primary location for all resources')
param location string

@description('Id of the principal to assign database and application roles')
param principalId string = ''

@description('Principal type (User, ServicePrincipal, Group)')
param principalType string = 'ServicePrincipal'

// Tags that should be applied to all resources
@description('Tags to be applied to all resources. Each resource will receive these tags plus the SecurityControl:Ignore tag.')
param tags object = {
  'azd-env-name': environmentName
  SecurityControl: 'Ignore'
}

// Resource naming parameters
var abbrs = loadJsonContent('./abbreviations.json')
var resourceToken = toLower(uniqueString(subscription().id, environmentName, location))

// Resource group
resource rg 'Microsoft.Resources/resourceGroups@2024-03-01' = {
  name: '${abbrs.resourcesResourceGroups}${environmentName}'
  location: location
  tags: tags
}

// Monitoring resources (Log Analytics + Application Insights)
module monitoring './modules/monitoring.bicep' = {
  name: 'monitoring-${resourceToken}'
  scope: rg
  params: {
    location: location
    tags: tags
    logAnalyticsName: '${abbrs.operationalInsightsWorkspaces}${resourceToken}'
    applicationInsightsName: '${abbrs.insightsComponents}${resourceToken}'
  }
}

// Key Vault for secrets management
module keyVault './modules/keyvault.bicep' = {
  name: 'keyvault-${resourceToken}'
  scope: rg
  params: {
    location: location
    tags: tags
    keyVaultName: '${abbrs.keyVaultVaults}${resourceToken}'
    principalId: principalId
  }
}

// Azure SQL Database
module database './modules/database.bicep' = {
  name: 'database-${resourceToken}'
  scope: rg
  params: {
    location: location
    tags: tags
    sqlServerName: '${abbrs.sqlServers}${resourceToken}'
    sqlDatabaseName: 'sqldb-healthcare'
    keyVaultName: keyVault.outputs.name
    allowAzureServices: true
  }
}

// App Service Plan and Web App
module appService './modules/app-service.bicep' = {
  name: 'app-service-${resourceToken}'
  scope: rg
  params: {
    location: location
    tags: tags
    appServicePlanName: '${abbrs.webServerFarms}${resourceToken}'
    appServiceName: '${abbrs.webSitesAppService}${resourceToken}'
    applicationInsightsConnectionString: monitoring.outputs.applicationInsightsConnectionString
    keyVaultName: keyVault.outputs.name
    sqlConnectionStringSecretName: database.outputs.connectionStringSecretName
  }
}

// Outputs for azd
output AZURE_LOCATION string = location
output AZURE_TENANT_ID string = tenant().tenantId
output AZURE_RESOURCE_GROUP string = rg.name

output APPLICATIONINSIGHTS_CONNECTION_STRING string = monitoring.outputs.applicationInsightsConnectionString
output APPLICATIONINSIGHTS_NAME string = monitoring.outputs.applicationInsightsName

output AZURE_KEY_VAULT_ENDPOINT string = keyVault.outputs.endpoint
output AZURE_KEY_VAULT_NAME string = keyVault.outputs.name

output AZURE_SQL_SERVER_NAME string = database.outputs.sqlServerName
output AZURE_SQL_DATABASE_NAME string = database.outputs.sqlDatabaseName

output WEB_APP_NAME string = appService.outputs.appServiceName
output WEB_APP_URI string = appService.outputs.uri
