using Flurl.Http;

namespace AZ204_EntrAuth.HttpClient
{
	public class GraphHttpClient : IGraphHttpClient
	{
		const string GRAPH_URL = "https://graph.microsoft.com/v1.0";

		public GraphHttpClient()
		{
		}

		public async Task<string> Get(string accessToken, string? restOfUrl = null)
		{
			try
			{
				if (!string.IsNullOrEmpty(restOfUrl))
				{
					return await $"{GRAPH_URL}/{restOfUrl}".WithOAuthBearerToken(accessToken).GetStringAsync();
				}

				return await GRAPH_URL.WithOAuthBearerToken(accessToken).GetStringAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return "error";
		}

		public async Task<string> Post(string accessToken, object data)
		{
			try
			{
				return await GRAPH_URL.WithOAuthBearerToken(accessToken).PostJsonAsync(data).ReceiveString();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return "error";
		}

		public async Task<string> Put(string accessToken, object data)
		{
			try
			{
				return await GRAPH_URL.WithOAuthBearerToken(accessToken).PutJsonAsync(data).ReceiveString();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return "error";
		}

		public async Task<string> Delete(string accessToken)
		{
			try
			{
				return await GRAPH_URL.WithOAuthBearerToken(accessToken).DeleteAsync().ReceiveString();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return "error";
		}
	}
}
