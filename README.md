keyvault_pipeline
Projeto de azure pipeline com integracao com Azure key vault.

O Azure Key Vault permite que os desenvolvedores armazenem e gerenciem com segurança informações confidenciais.

pipeline yaml

parameters:
- name: FilesSettings
  type: string
  default: |
         **/appsettings*.json  #NOME DOS SETTINGS COM SUFIXOS
         **/sharedSettings*.json 

jobs:
- job:
  displayName: replace

  pool:
    name: Azure Pipelines

  steps:
  - task: AzureKeyVault@2
    displayName: 'Azure Key Vault: poc-keyvault-joaopma'
    inputs:
      azureSubscription: portal
      KeyVaultName: 'poc-keyvault-joaopma' #NOME DO RECURSO CRIADO NO PORTAL
      SecretsFilter: '*'
     
  - task: qetza.replacetokens.replacetokens-task.replacetokens@6 #TASK DE REPLACE TOKENS(SUFIXOS)
    displayName: 'Replace tokens in appsettings.json'
    inputs:
      sources: ${{parameters.FilesSettings}}
      transforms: true
      logLevel: debug
      missingVarLog: error
Exemplo de arquivo **/appsettings.json

{
  "ConnectionStrings": {
    "SqlServer": "#{ConnectionStrings-SqlServer}#",
    "ServiceBus": "#{ConnectionStrings-ServiceBus}#",
    "Redis": "#{ConnectionStrings-Redis}#"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Debug",
      "Microsoft.Hosting.Lifetime": "Debug"
    }
  },
  "AllowedHosts": "*"
}
