namespace OpenBrisk.Runtime.Core.Interfaces
{
    using Microsoft.CodeAnalysis;
    
    public interface IReferencesManager
    {
        MetadataReference[] GetReferences();
    }
}
