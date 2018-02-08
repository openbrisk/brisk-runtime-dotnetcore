# OpenBrisk .NET Core 2.0 Runtime

A brisk runtime for .NET Core 2.0 functions written in C#.

```csharp
namespace Functions
{
    using System;
    using OpenBrisk.Runtime.Shared;

    public class HelloWorld
    {
        public string Execute(IBriskContext context)
        {
            return "Hello World!";
        }
    }
}
```

## Local Development

If you want to start the server for a function in the `examples` folder you must
export the environment variables `MODULE_NAME` and `FUNCTION_HANDLER`. You can
do it manually or by creating a `lauchSettings.json` file in the folder `OpenBrisk.Runtime/Properties`.
Here is an example settings file that confgures the `HelloWorld` function for
the server. Please note that this only works if the dotnet environemtn is set to
`Development` using the `ASPNETCORE_ENVIRONMENT` variable, because this will force
the service to look for the function code in the `examples` folder.

```json
{
    "profiles": {
        "OpenBrisk.Runtime": {
            "commandName": "Project",
            "launchBrowser": false,
            "launchUrl": "api/values",
            "environmentVariables": {
                "ASPNETCORE_ENVIRONMENT": "Development",
                "MODULE_NAME": "HelloWorld",
                "FUNCTION_HANDLER": "Execute"
            },
            "applicationUrl": "http://localhost:8080/"
        }
    }
}
```
