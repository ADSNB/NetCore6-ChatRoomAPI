namespace Domain.Models
{
    public class AppSettingsModel
    {
        public AzureAD AzureAD { get; set; }
        public Auth0 Auth0 { get; set; }
        public Database Database { get; set; }
        public SignalR SignalR { get; set; }
        public string RobotExcelSpreadsheetLocation { get; set; }
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

    public class Auth0
    {
        public string Domain { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
    }

    public class Database
    {
        public bool InMemoryEnabled { get; set; }
        public string ConnectionString { get; set; }
    }

    public class SignalR
    {
        public string BaseUrl { get; set; }
        public string HubName { get; set; }
    }
}