
parameters:
- name: FilesSettings
  type: string
  default: |
         **/appsettings*.json #NOME DOS ENV ONDE CONTEM OS SUFIXOS
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
