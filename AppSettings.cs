namespace AZ204_EntrAuth
{
    public class AppSettings
    {
        public ClientApplicationSettings AzureSettings { get; set; }
    }

    public class ClientApplicationSettings
    {
        public ClientSettings PublicClient { get; set; }
        public ConfidentialCLientSettings ConfidentialClient { get; set; }

    }

    public class ClientSettings
    {
        public string ClientId { get; set; }
        public string TenantId { get; set; }
    }

    public class ConfidentialCLientSettings : ClientSettings
    {
        public string Secret { get; set; }
    }
}
