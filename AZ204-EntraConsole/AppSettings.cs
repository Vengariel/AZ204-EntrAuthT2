namespace AZ204_EntrAuth
{
	public class AppSettings
	{
		public ClientApplicationSettings AzureSettings { get; set; }
	}

	public class ClientApplicationSettings
	{
		public ConfidentialClientSettings ConfidentialClient { get; set; }
		public string KeyVaultUrl { get; set; }
	}

	public class ConfidentialClientSettings
	{
		public string[]? Scopes { get; set; }
	}
}
