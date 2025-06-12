keyvault_pipeline
Projeto de azure pipeline com integracao com Azure key vault.

O Azure Key Vault permite que os desenvolvedores armazenem e gerenciem com segurança informações confidenciais.


pipeline yaml

```
parameters:
- name: FilesSettings
  type: string
  default: |
         **/appsettings*.json
         **/sharedSettings*.json 

  
jobs:
- job:
  displayName: replace

  pool:
    name: Azure Pipelines

#AQUI VOCE ENTRA COM AS REGRA DE BRANCH/AMBIENTE versus O RECURSO DE KEYVAULT COM AS DEVIDAS CREDENCIAS
  variables:
    - ${{ if eq(variables['Build.SourceBranchName'], 'main')}}:
        - name: RECURSO_KEYVAULT
          value: 'RECURSO_KEYVAULT_PRODUCAO'
    - ${{ if eq(variables['Build.SourceBranchName'], 'homolog')}}:
        - name: RECURSO_KEYVAULT
          value: 'RECURSO_KEYVAULT_HOMOLOGACAO'

  
  steps:
  - task: AzureKeyVault@2
    displayName: 'Azure Key Vault: poc-keyvault-joaopma'
    inputs:
      azureSubscription: portal
      KeyVaultName: ${{variables.RECURSO_KEYVAULT}}
      SecretsFilter: '*'


  - powershell: |    
      $connectionstring = "$(ConnectionStrings-ServiceBus)"
      Write-Host $connectionstring.ToCharArray()
    displayName: 'keyvault secret Script'
    
  - task: qetza.replacetokens.replacetokens-task.replacetokens@6
    displayName: 'Replace tokens in appsettings.json'
    inputs:
      sources: ${{parameters.FilesSettings}}
      transforms: true
      logLevel: debug
      missingVarLog: error
```

```
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
```
