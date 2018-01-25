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
