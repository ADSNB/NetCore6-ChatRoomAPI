namespace Domain.Models
{
    public class AppSettingsModel
    {
        public AzureAD AzureAD { get; set; }
        public DatabaseConfiguration DatabaseConfiguration { get; set; }
    }

    public class AzureAD
    {
        public string Instance { get; set; }
        public string Domain { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string Scopes { get; set; }
        public string CallbackPath { get; set; }
    }

    public class DatabaseConfiguration
    {
        public bool InMemoryEnabled { get; set; }
        public string ConnectionString { get; set; }
    }
}