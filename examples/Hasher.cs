namespace Functions
{
    using System;
    using OpenBrisk.Runtime.Shared;
    
    public class Hasher
    {
        public string Execute(IBriskContext context)
        {
            return BCrypt.Net.BCrypt.HashPassword("Hello World!", 10);
        }
    }
}
