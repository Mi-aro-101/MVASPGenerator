using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generator.Object
{
    public class ClassObject
    {
        public ClassDeclarationSyntax? Class { get; set; }

        public PropertyDeclarationSyntax? PrimaryKey { get; set; }
        
        public PropertyDeclarationSyntax FindPk()
        {
            PropertyDeclarationSyntax pk = null;
            PropertyDeclarationSyntax[] fields = this.Class.Members.OfType<PropertyDeclarationSyntax>().ToArray();
                foreach(var field in fields)
                {
                    if(IsAnnotated(field, "Key"))
                    {
                        pk = field;
                        break;
                    }
                }
            this.PrimaryKey = pk;
            return pk;
        }

        public PropertyDeclarationSyntax[] FindAnnotatedProperties(string annotation)
        {
            List<PropertyDeclarationSyntax> result = new List<PropertyDeclarationSyntax>();
            PropertyDeclarationSyntax[] fields = this.Class.Members.OfType<PropertyDeclarationSyntax>().ToArray();
            foreach(var field in fields)
            {
                if(IsAnnotated(field, annotation))
                {
                    result.Add(field);
                }
            }
            return result.ToArray();
        }

        public bool IsAnnotated(PropertyDeclarationSyntax field, string annotation)
        {
            bool result = false;
            foreach(var attributeList in field.AttributeLists)
            {
                foreach(var attribute in attributeList.Attributes)
                {
                    var attributeName = attribute.Name.ToString();
                    if(attributeName == annotation || attributeName.EndsWith("."+annotation))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public PropertyDeclarationSyntax[] FindNotPkProps()
        {
            List<PropertyDeclarationSyntax> results = this.Class.Members.OfType<PropertyDeclarationSyntax>().ToList();
            results.Remove(this.PrimaryKey);
            return results.ToArray();
        }
    }
}
