using Generator.Object;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generator.Utils;

public class ClassManager
{
    public ClassDeclarationSyntax[]? Classes { get; set; }

    public ClassDeclarationSyntax[] ExtractClassesFromRawString(string code)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);
        CompilationUnitSyntax root = syntaxTree.GetCompilationUnitRoot();
        var classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
        this.Classes = classDeclarations.ToList().ToArray();
        return classDeclarations.ToList().ToArray();
    }

    public ClassObject LoadOneClass(int id)
    {
        ClassObject obj = new ClassObject();
        obj.Class = this.Classes[id];
        obj.FindPk();
        return obj;
    }

    public static string FirstToLowerCase(string word)
    {
        return char.ToLower(word[0]) + word.Substring(1);
    }
}
