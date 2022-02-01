# Sudoku Game API with Microsoft Identity Platform using Azure AD B2C and Azure GraphAPI


Example appsettings.json file
```sh
{
  "AzureAdB2C": {
    "Instance": "",
    "ClientId": "",
    "Domain": "",
    "SignUpSignInPolicyId": ""
  },
  "AzureGraph": {
    "TenantId": "",
    "AppId": "",
    "ClientSecret": "",
    "B2cExtensionAppClientId": ""
  },
  "ConnectionStrings": {
    "Local": "Data Source=XXX;Initial Catalog=XXX;Integrated Security=True",
    "Server": ""
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```