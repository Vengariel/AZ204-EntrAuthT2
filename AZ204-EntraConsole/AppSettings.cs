namespace AZ204_EntrAuth
{
	public class AppSettings
	{
		public ClientApplicationSettings AzureSettings { get; set; }
	}

	public class ClientApplicationSettings
	{
		public ClientSettings PublicClient { get; set; }
		public ClientSettings PublicClientDeviceCode { get; set; }
		public ConfidentialClientSettings ConfidentialClient { get; set; }
		public string RedirectUrl { get; set; }
	}

	public class ClientSettings
	{
		public string ClientId { get; set; }
		public string TenantId { get; set; }
	}

	public class ConfidentialClientSettings : ClientSettings
	{
		public string Secret { get; set; }
		public string[]? Scopes { get; set; }
	}
}
