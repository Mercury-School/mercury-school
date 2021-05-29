param sqlServerParameters object

resource server 'Microsoft.Sql/servers@2020-11-01-preview' = {
  name: sqlServerParameters.serverName
  location: resourceGroup().location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    administratorLogin: sqlServerParameters.administratorLogin
    administratorLoginPassword: sqlServerParameters.administratorLoginPassword
  }
}

resource database 'Microsoft.Sql/servers/databases@2020-11-01-preview' = {
  name: '${server.name}/${sqlServerParameters.databaseName}'
  location: resourceGroup().location
  sku: {
    name: 'Basic'
    tier: 'Basic'
  }
}

resource firewallRuleAzureService 'Microsoft.Sql/servers/firewallRules@2020-11-01-preview' = {
  name: '${server.name}/AllowAllWindowsAzureIps'
  properties: {
    startIpAddress: '0.0.0.0'
    endIpAddress: '0.0.0.0'
  }
}
