namespace Presentation.WPF.Configuration
{
    public class DatabaseSettings
    {
        public string Provider { get; set; }
        public Dictionary<string, string> ConnectionStrings { get; set; }
    }
}
