using Generator.Object;
using Generator.Utils;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Main;

class Program
{
    static void Main(string[] args)
    {        
        VCGenerator vcGenerator = new VCGenerator();
        ClassObject table = vcGenerator.GetClassObject();

        // Test
        Console.WriteLine("The class : " + table.Class.Identifier.Text);
        Console.WriteLine("The primary key : "+table.PrimaryKey.Identifier.Text);
    }
}
