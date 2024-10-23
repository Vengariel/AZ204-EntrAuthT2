using AZ204_EntrAuth;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace AZ204_EntraAPI.Services;

public class KeyVaultService
{
	private readonly SecretClient _secretClient;

	public KeyVaultService(ClientApplicationSettings clientApplicationSettings)
	{
		var keyVaultUrl = clientApplicationSettings.KeyVaultUrl ?? throw new NullReferenceException();
		var vaultUri = new Uri(keyVaultUrl);
		_secretClient = new SecretClient(vaultUri, new DefaultAzureCredential());
	}

	public async Task<string> GetSecretAsync(string secretName)
	{
		KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);
		return secret.Value;
	}
}
