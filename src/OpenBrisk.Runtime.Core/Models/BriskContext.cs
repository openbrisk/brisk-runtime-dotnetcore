namespace OpenBrisk.Runtime.Core.Models
{
    using System.Dynamic;
    
    public sealed class BriskContext : DynamicObject
    {
        public dynamic Data { get; set; }      
    }
}