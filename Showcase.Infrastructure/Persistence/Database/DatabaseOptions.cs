namespace Showcase.Infrastructure.Persistence.Database
{
    public class DatabaseOptions
    {
        public const string SectionKey = "Database";

        public string DbPath { get; set; } = String.Empty;
    }
}
