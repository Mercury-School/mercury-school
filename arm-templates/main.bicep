param resourceGroupName string
param adminObjectId string

var vnetName = '${resourceGroupName}-vnet'
var storageAccountName = 'storage${uniqueString(resourceGroupName)}'
var functionAppServiceName = '${resourceGroupName}-functions'
var keyVaultName = '${resourceGroupName}-key-vault'
var cosmosDbName = '${resourceGroupName}-cosmos-db'
var applicationGatewayName = '${resourceGroupName}-application-gateway'
var applicationGatewayIpName = '${resourceGroupName}-application-gateway-ip'

module vnetDeployment 'vnet/vnet-template.bicep' = {
  name: '${vnetName}-deployment'
  params: {
    vnetName: vnetName
  }
}

module iotHubDeployment 'iothub/iot-hub-template.bicep' = {
  name: 'iot-hub-deployment'
}

module applicationGatewayIpDeployment 'public-ip/public-ip-template.bicep' = {
  name: '${applicationGatewayIpName}-deployment'
  params: {
    publicIpName: applicationGatewayIpName
  }
}

module storageDeployment 'storage/storage-template.bicep' = {
  name: '${storageAccountName}-deployment'
  params: {
    storageAccountName: storageAccountName
    vnetDataSubNetId: vnetDeployment.outputs.vnetDataSubNetId
  }
}

module functionAppServiceDeployment 'app-service-plan/function-app-service-plan-template.bicep' = {
  name: '${functionAppServiceName}-deployment'
  params: {
    functionAppServiceName: functionAppServiceName
    vnetDataSubNetId: vnetDeployment.outputs.vnetDataSubNetId
  }
}

module cosmosDbDeployment 'cosmosdb/cosmosdb-template.bicep' = {
  name: '${cosmosDbName}-deployment'
  params: {
    cosmosDbName: cosmosDbName
    databaseName: 'school'
    vnetDataSubNetId: vnetDeployment.outputs.vnetDataSubNetId
  }
}

var adminPrincipleIds = [
  adminObjectId
  functionAppServiceDeployment.outputs.functionAppPrincipleId
  cosmosDbDeployment.outputs.cosmosDbPrincipleId
]

module keyVaultDeployment 'key-vault/key-vault-template.bicep' = {
  name: '${keyVaultName}-deployment'
  params: {
    adminPrincipleIds: adminPrincipleIds
    keyVaultName: keyVaultName
    vnetDataSubNetId: vnetDeployment.outputs.vnetDataSubNetId
  }
}
