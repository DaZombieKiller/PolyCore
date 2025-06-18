using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace PolyCore;

[Generator(LanguageNames.CSharp)]
public sealed class GlobalUsingGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(context =>
        {
            context.AddSource("PolyCore.GlobalUsings.g.cs", SourceText.From(
                """
                global using PolyCore.Internal;
                """, Encoding.UTF8));
        });
    }
}
