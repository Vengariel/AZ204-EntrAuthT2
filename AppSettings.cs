namespace AZ204_EntrAuth
{
    public class AppSettings
    {
        public Azure AzureSettings { get; set; }

    }

    public class Azure
    {
        public string ClientId { get; set; }
        public string TenantId { get; set; }

    }
}
