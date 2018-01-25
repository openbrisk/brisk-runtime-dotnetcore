namespace Functions
{
    using System;
    
    public class Hasher
    {
        public string Execute(dynamic context)
        {
            return BCrypt.Net.BCrypt.HashPassword("Hello World!", 10);
        }
    }
}
