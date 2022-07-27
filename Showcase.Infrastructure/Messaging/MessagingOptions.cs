namespace Showcase.Infrastructure.Messaging
{
    public class MessagingOptions
    {
        public const string SectionKey = "Messaging";

        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Hostname { get; set; } = String.Empty;
        public int Port { get; set; }
        public string Queue { get; set; } = String.Empty;
    }
}
