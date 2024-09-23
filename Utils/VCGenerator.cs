using Generator.Object;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generator.Utils
{
    public class VCGenerator
    {
        public ClassObject GetClassObject()
        {
            FileManager fileManager = new FileManager();
            ClassManager classManager = new ClassManager();
            Configuration configuration = FileManager.LoadGenerationConfig();

            string filePath = configuration.ProjectPath+configuration.ModelSubFolder+configuration.ModelName+".cs";
            // Extract as string the content of the C# file MODEL
            string code = fileManager.ExtractFileContent(filePath);
            // Extract All Classes inside the C# file MODEL
            ClassDeclarationSyntax[] classesInFile = classManager.ExtractClassesFromRawString(code);
            // Extract the Class (table) inside the C# file MODEL
            ClassObject classTable = classManager.LoadOneClass(0);

            // Generate the controller
            this.GenerateController(fileManager, classTable, configuration);

            //Generate the views
            ViewContentHandler viewContentHandler = new ViewContentHandler();
            this.GenerateIndexView(fileManager, classTable, configuration, viewContentHandler);
            this.GenerateCreateView(fileManager, classTable, configuration, viewContentHandler);
            this.GenerateEditView(fileManager, classTable, configuration, viewContentHandler);

            return classTable;
        }

        public void GenerateController(FileManager fileManager, ClassObject classObject, Configuration configuration)
        {
            // Generate the content of the controller to write in the file
            ControllerContentHandler contentCreator = new ControllerContentHandler();
            string controllerContent = contentCreator.GenerateControllerContent(fileManager, classObject, configuration);
            // Generate the controller file
            string controllerFileName = configuration.ModelName+"Controller.cs";
            string controllerPath = configuration.ProjectPath+configuration.ControllerSubFolder;
            fileManager.CreateFile(controllerPath, controllerFileName);
            string controllerFile = controllerPath+controllerFileName;
            fileManager.WriteFileContent(controllerFile, controllerContent);
        }

        public void GenerateIndexView(FileManager fileManager, ClassObject classObject, Configuration configuration, ViewContentHandler viewContentHandler)
        {
            string indexContent = viewContentHandler.GenerateIndexContent(configuration, classObject, fileManager);
            // Generate the index view file
            string indexFileName = "Index.cshtml";
            string indexPath = configuration.ProjectPath+configuration.ViewSubFolder+classObject.Class.Identifier.Text;
            fileManager.CreateDirectory(indexPath);
            fileManager.CreateFile(indexPath, indexFileName);
            string indexFile = indexPath+"/"+indexFileName;
            Console.WriteLine(indexFile);
            fileManager.WriteFileContent(indexFile, indexContent);
        }

        public void GenerateCreateView(FileManager fileManager, ClassObject classObject, Configuration configuration, ViewContentHandler viewContentHandler)
        {
            string indexContent = viewContentHandler.GenerateCreateContent(configuration, classObject, fileManager);
            // Generate the index view file
            string indexFileName = "Create.cshtml";
            string indexPath = configuration.ProjectPath+configuration.ViewSubFolder+classObject.Class.Identifier.Text;
            fileManager.CreateFile(indexPath, indexFileName);
            string indexFile = indexPath+"/"+indexFileName;
            Console.WriteLine(indexFile);
            fileManager.WriteFileContent(indexFile, indexContent);
        }

        public void GenerateEditView(FileManager fileManager, ClassObject classObject, Configuration configuration, ViewContentHandler viewContentHandler)
        {
            string indexContent = viewContentHandler.GenerateEditContent(configuration, classObject, fileManager);
            // Generate the index view file
            string indexFileName = "Edit.cshtml";
            string indexPath = configuration.ProjectPath+configuration.ViewSubFolder+classObject.Class.Identifier.Text;
            fileManager.CreateFile(indexPath, indexFileName);
            string indexFile = indexPath+"/"+indexFileName;
            Console.WriteLine(indexFile);
            fileManager.WriteFileContent(indexFile, indexContent);
        }
    }
}