using Generator.Object;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generator.Utils
{
    public class ViewContentHandler
    {
        public string? ViewType { get; set; }

        public string GenerateColumnName(ClassObject classObject)
        {
            string result = "";
            PropertyDeclarationSyntax[] fields = classObject.Class.Members.OfType<PropertyDeclarationSyntax>().ToArray();
            foreach(var field in fields)
            {
                result += "<th>@Html.DisplayNameFor(model => model."+field.Identifier.Text+")</th>\n\t\t\t";
            }
            return result;
        }

        public string GenerateColumnValues(ClassObject classObject)
        {
            string result = "";
            PropertyDeclarationSyntax[] fields = classObject.Class.Members.OfType<PropertyDeclarationSyntax>().ToArray();
            foreach(var field in fields)
            {
                result += "<td>@Html.DisplayFor(modelItem => item."+field.Identifier.Text+")</td>\n\t\t\t";
            }
            return result;
        }

        public string GenerateIndexContent(Configuration configuration, ClassObject classObject, FileManager fileManager)
        {
            string result = "";
            string templateContent = fileManager.ExtractFileContent("./resources/template/view/"+configuration.ViewType.ToLower()+"/index.cst");
            result = templateContent.Replace("#model#", configuration.ModelName);
            result = result.Replace("#project#", configuration.ProjectName);
            result = result.Replace("#modelNamespace#", configuration.ModelNamespace);
            result = result.Replace("#columnName#", this.GenerateColumnName(classObject));
            result = result.Replace("#columnValues#", this.GenerateColumnValues(classObject));
            result = result.Replace("#pk#", classObject.PrimaryKey.Identifier.Text);
            return result;
        }

        public string GenerateCreateContent(Configuration configuration, ClassObject classObject, FileManager fileManager)
        {
            string result = "";
            string templateContent = fileManager.ExtractFileContent("./resources/template/view/"+configuration.ViewType.ToLower()+"/create.cst");
            result = templateContent.Replace("#formGroup#", this.GenerateFormGroup(classObject));
            result = result.Replace("#model#", configuration.ModelName);
            result = result.Replace("#project#", configuration.ProjectName);
            result = result.Replace("#modelNamespace#", configuration.ModelNamespace);

            return result;
        }

        public string GenerateEditContent(Configuration configuration, ClassObject classObject, FileManager fileManager)
        {
            string result = "";
            string templateContent = fileManager.ExtractFileContent("./resources/template/view/"+configuration.ViewType.ToLower()+"/edit.cst");
            result = templateContent.Replace("#formGroup#", this.GenerateFormGroup(classObject));
            result = result.Replace("#model#", configuration.ModelName);
            result = result.Replace("#project#", configuration.ProjectName);
            result = result.Replace("#modelNamespace#", configuration.ModelNamespace);
            result = result.Replace("#pk#", classObject.PrimaryKey.Identifier.Text);

            return result;
        }

        public string GenerateFormGroup(ClassObject classObject)
        {
            string result= "";
            FormConfig formConfig = FileManager.LoadFormConfig();
            ViewConfig viewConfig = FileManager.LoadViewConfig();
            PropertyDeclarationSyntax[] fields = classObject.FindNotPkProps();
            foreach(var field in fields)
            {
                result += formConfig.FormGroup+"\n";
                result = result.Replace("#input#", viewConfig.Singular);
                result = result.Replace("#property#", field.Identifier.Text);
            }
            return result;
        }
    }
}