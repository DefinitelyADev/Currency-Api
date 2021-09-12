# Currency API

A RESTful API for managing and calculating currency rates built using .Net 5.

## Data settings

In order to run this application, you'll need to edit the `appDataSettings.json` file.

Example data configuration file:

```json
{
    "ConnectionString": "Server=127.0.0.1;Port=3306;Database=currency_api;AllowUserVariables=True;User Id=root;",
    "DataProvider": "MySql",
    "EnableSensitiveDataLogging": true,
    "RawDataSettings": {}
}
```
* The connection string should be in the format supported the respective provider. [More info](https://www.connectionstrings.com/)
* The supported providers are `MySql`, `SqlServer` and `Postgres`.

## Using

Note: There is no need for pre-installing a runtime as the executables are bulit as self-contained

### Windows

1. Download the windows archive from the releases page
2. Extract the downloaded archive to the desired directory
3. Replace {publish dir} with the export directory and run the following command

```ps
env:DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1; {publish dir}/CurrencyApi.Presentation.exe --urls http://127.0.0.1:5000
```
### Linux

1. Download the linux archive from the releases page
2. Extract the downloaded archive to the desired directory
3. Replace {ver} with the downloaded version and {publish dir} with the export directory
4. Run the following commands

Install dependencies: `unzip`

```bash
unzip currency-api-v{ver}-linux-amd64.zip -d {publish dir}
chmod +x {publish dir}/CurrencyApi.Presentation
cd {publish dir}
DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 ./CurrencyApi.Presentation --urls http://127.0.0.1:5000
```

Next [After run](#after-run)

## Running from source

Note: When running in debug mode the migrations have to be applied manually.
To apply the migrations manually please run the following command, in the parent directory of the project.
```dotnetcli
dotnet ef database update --startup-project .\Src\CurrencyApi.Presentation\
```
More info: [Entity Framework Core tools reference - .NET Core CLI](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

## After run

After starting the application you should be automatically directed at the swagger ui. If this doesn't happen you can navigate manualy by typing {BaseUrl}/swagger in your preferred browser.

In order to use the api you must first login.
The default credentials are:

* Admin user:
    - username: `admin`
    - password: `defaultAdminPass1!`

* Simple user:
    - username: `user`
    - password: `defaultUserPass1!`
