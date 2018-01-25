namespace Functions
{
    using System;
    
    public class Echo
    {
        public object Execute(dynamic context)
        {
            return context.Data.text;
        }
    }
}
