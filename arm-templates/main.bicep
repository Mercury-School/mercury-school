param runid string
param administratorLogin string
param administratorLoginPassword string
param adAdminlogin string
param adAdminSid string
param adAdminTenantId string

var resourceGroupName = resourceGroup().name
var vnetName = '${resourceGroupName}-vnet'

var dnsZoneName = '${resourceGroupName}.cloud'
// var appServicePlanName = '${resourceGroupName}-app-service-plan'
// var webAppName = '${resourceGroupName}-web-api'

var sqlServerParameters = {
  'serverName': '${resourceGroupName}-sql-server'
  'databaseName': 'MercurySchool'
  'administratorLogin': administratorLogin
  'administratorLoginPassword': administratorLoginPassword
  'adAdminlogin': adAdminlogin
  'adAdminSid': adAdminSid
  'adAdminTenantId': adAdminTenantId
}

module vnetDeployment 'vnet/vnet-template.bicep' = {
  name: '${vnetName}-deployment-${runid}'
  params: {
    vnetName: vnetName
  }
}

module dnsZoneDeployment 'private-dns-zone/private-dns-zone-template.bicep' = {
  name: '${dnsZoneName}-dns-zone-deployment-${runid}'
  params: {
    dnsZoneName: dnsZoneName
    ventId: vnetDeployment.outputs.vnetId
  }
}

module sqlServerDeployment 'sql-server/sql-server-template.bicep' = {
  name: '${sqlServerParameters.serverName}-deployment-${runid}'
  params: {
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorLoginPassword
    serverName: '${resourceGroupName}-sql-server'
  }
}

// module appServicePlanDeployment 'app-service-plan/web-app-service-plan-template.bicep' = {
//   name: '${appServicePlanName}-deployment'
//   params: {
//     appServicePlanName: appServicePlanName
//     webAppName: webAppName
//     // vnetWebSubNetId: vnetDeployment.outputs.vnetWebSubNetId
//     // vnetName: vnetName
//   }
// }

// module privateEndpointDeployment 'private-endpoint/private-endpoint-template.bicep' = {
//   name: '${privateEndpointName}-deployment'
//   params: {
//     privateEndpointName: privateEndpointName
//     subNetId: vnetDeployment.outputs.vnetDataSubNetId
//     webAppId: appServicePlanDeployment.outputs.webApiId
//   }
// }
