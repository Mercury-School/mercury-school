param appServicePlanName string
param webAppName string
// param vnetWebSubNetId string
// param vnetName string

resource appServicePlan 'Microsoft.Web/serverfarms@2020-12-01' = {
  name: appServicePlanName
  location: resourceGroup().location
  kind: 'linux'
  sku: {
    name: 'P1v2'
    tier: 'PremiumV2'
  }
  properties: {
    reserved: true
  }
}

resource webApp 'Microsoft.Web/sites@2020-12-01' = {
  name: webAppName
  location: resourceGroup().location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: appServicePlan.id

    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|5.0'
      ftpsState: 'Disabled'
      appSettings: [
        {
          name: 'WEBSITE_VNET_ROUTE_ALL'
          value: '1'
        }
        {
          name: 'WEBSITE_DNS_SERVER'
          value: '168.63.129.16'
        }
      ]
    }
  }
}

output webApiId string = webApp.id
