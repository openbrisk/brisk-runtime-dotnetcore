namespace OpenBrisk.Runtime.Core.Models
{
    using OpenBrisk.Runtime.Core.Interfaces;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class DefaultParser : IParser
    {
        public SyntaxTree ParseText(string code)
        {
            return CSharpSyntaxTree.ParseText(code);
        }
    }
}
