using System.Text.Json;
using Generator.Object;

namespace Generator.Utils
{
    public class FileManager
    {
        public static Configuration LoadGenerationConfig()
        {
            Configuration config = null;
            using(StreamReader sr = new StreamReader("./resources/config.json"))
            {
                string json = sr.ReadToEnd();
                config = JsonSerializer.Deserialize<Configuration>(json);
            }
            return config;
        }

        public static FormConfig LoadFormConfig()
        {
            FormConfig form = null;
            using(StreamReader sr = new StreamReader("./resources/formconfig.json"))
            {
                string json = sr.ReadToEnd();
                form = JsonSerializer.Deserialize<FormConfig>(json);
            }
            return form;
        }

        public static ViewConfig LoadViewConfig()
        {
            ViewConfig viewConfig = null;
            using(StreamReader sr = new StreamReader("./resources/viewconfig.json"))
            {
                string json = sr.ReadToEnd();
                viewConfig = JsonSerializer.Deserialize<ViewConfig>(json);
            }
            return viewConfig;
        }
        public string ExtractFileContent(string filePath)
        {
            string result = "";

            result = File.ReadAllText(filePath);
            return result;
        }

        public void CreateFile(string filePath, string fileName)
        {
            FileStream file = File.Create(filePath+fileName);
            file.Close();
        }

        public void CreateDirectory(string directoryPath)
        {
            DirectoryInfo info = new DirectoryInfo(directoryPath);
            if(!Directory.Exists(directoryPath))
            {
                info.Create();
            }
            else if (Directory.Exists(directoryPath))
            {
                foreach(FileInfo file in info.GetFiles())
                {
                    file.Delete();
                }
                Directory.Delete(directoryPath);
                info.Create();
            }
        }

        public void WriteFileContent(string file, string content)
        {
            File.WriteAllText(file, content);
        }
    }
}