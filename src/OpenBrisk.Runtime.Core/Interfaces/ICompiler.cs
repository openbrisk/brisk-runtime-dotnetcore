﻿namespace OpenBrisk.Runtime.Core.Interfaces
{
    public interface ICompiler
    {
        IParser Parser { get; }
        
        IReferencesManager ReferencesManager { get; }

        void Compile(IFunction function);
    }
}
