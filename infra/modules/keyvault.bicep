@description('The location for all resources')
param location string

@description('Tags to apply to all resources')
param tags object

@description('Name of the Key Vault')
param keyVaultName string

@description('Principal ID to grant access')
param principalId string

@description('Principal type (User or ServicePrincipal)')
param principalType string = 'User'

// Key Vault
resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' = {
  name: keyVaultName
  location: location
  tags: tags
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: subscription().tenantId
    enableRbacAuthorization: true
    enableSoftDelete: true
    softDeleteRetentionInDays: 90
    enabledForDeployment: false
    enabledForDiskEncryption: false
    enabledForTemplateDeployment: true
    publicNetworkAccess: 'Enabled'
    networkAcls: {
      bypass: 'AzureServices'
      defaultAction: 'Allow'
    }
  }
}

// Role assignment for the principal (Key Vault Secrets Officer)
var keyVaultSecretsOfficerRole = subscriptionResourceId('Microsoft.Authorization/roleDefinitions', 'b86a8fe4-44ce-4948-aee5-eccb2c155cd7')

resource keyVaultRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (!empty(principalId)) {
  scope: keyVault
  name: guid(keyVault.id, principalId, keyVaultSecretsOfficerRole)
  properties: {
    roleDefinitionId: keyVaultSecretsOfficerRole
    principalId: principalId
    principalType: principalType
  }
}

output id string = keyVault.id
output name string = keyVault.name
output endpoint string = keyVault.properties.vaultUri
