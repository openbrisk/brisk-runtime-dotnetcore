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