namespace Generator.Object
{
    public class Configuration
    {
        public string? DbSet { get; set; }
        public string? ProjectName { get; set; }
        public string? ContextNamespace { get; set; }
        public string? ModelNamespace { get; set; }
        public string? ContextName { get; set; }
        public string? ModelName { get; set; }
        public string? ProjectPath { get; set; }
        public string? ControllerSubFolder { get; set; }
        public string? ViewSubFolder { get; set; }
        public string? ModelSubFolder { get; set; }
        public string? ViewType { get; set; }
    }
}