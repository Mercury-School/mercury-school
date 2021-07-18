param serverName string
param administratorLogin string
param administratorLoginPassword string
param databaseName string
param adAdminlogin string
param adAdminSid string
param adAdminTenantId string

resource server 'Microsoft.Sql/servers@2020-11-01-preview' = {
  name: serverName
  location: resourceGroup().location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorLoginPassword
  }
}

resource database 'Microsoft.Sql/servers/databases@2020-11-01-preview' = {
  name: '${server.name}/${databaseName}'
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

resource firewallRuleAzureService2 'Microsoft.Sql/servers/firewallRules@2020-11-01-preview' = {
  name: '${server.name}/MikeHendersonHomeOffice'
  properties: {
    startIpAddress: '174.84.162.196'
    endIpAddress: '174.84.162.196'
  }
}

resource activeDiractoryAdmin 'Microsoft.Sql/servers/administrators@2021-02-01-preview' = {
  name: '${server.name}/ActiveDirectory'
  properties: {
    administratorType: 'ActiveDirectory'
    login: adAdminlogin
    sid: adAdminSid
    tenantId: adAdminTenantId
  }
}
