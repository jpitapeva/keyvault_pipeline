# Este script parte do recurso do Azure Key Vault

#ver secret 
$resource = "subsc-vs-key-vault"
az keyvault secret show --name "teste" --vault-name $resource



# Este script parte do recurso do Azure Key Vault
 #ver key
$resource = "subsc-vs-key-vault"
az keyvault key show --name "teste" --vault-name $resource