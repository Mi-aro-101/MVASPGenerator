using Generator.Object;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generator.Utils
{
    public class ControllerContentHandler
    {
        public string GenerateControllerContent(FileManager fileManager, ClassObject classObject, Configuration configuration)
        {
            string result = "";
            string templateContent = fileManager.ExtractFileContent("./resources/template/controller/controller.cst");
            string className = classObject.Class.Identifier.Text;
            result = templateContent.Replace("#model#", className);
            result = result.Replace("#pk#", classObject.PrimaryKey.Identifier.Text);
            result = result.Replace("#modelfirstlower#", ClassManager.FirstToLowerCase(className));
            result = result.Replace("#context#", configuration.ContextName);
            result = result.Replace("#bind#", this.BindProperty(classObject));
            result = result.Replace("#bindPost#", this.BindPostProperty(classObject));
            result = result.Replace("#modelNamespace#", configuration.ProjectName+"."+configuration.ModelNamespace);
            result = result.Replace("#contextNamespace#", configuration.ProjectName+"."+configuration.ContextNamespace);
            result = result.Replace("#project#", configuration.ProjectName);
            result = result.Replace("#dbSet#", configuration.DbSet);

            return result;
        }

        public string BindProperty(ClassObject classObject)
        {
            string result = "";
            result = "[Bind(\"";
            PropertyDeclarationSyntax[] fields = classObject.FindNotPkProps();
            foreach(var field in fields){
                result += field.Identifier.Text+",";
            }
            result = result.Remove(result.Length-1);
            result += "\")]";
            return result;
        }

        public string BindPostProperty(ClassObject classObject)
        {
            string result = "";
            result = "[Bind(\"";
            PropertyDeclarationSyntax[] fields = classObject.FindAnnotatedProperties("Column");
            foreach(var field in fields){
                result += field.Identifier.Text+",";
            }
            result = result.Remove(result.Length-1);
            result += "\")]";
            return result;
        }
    }
}
