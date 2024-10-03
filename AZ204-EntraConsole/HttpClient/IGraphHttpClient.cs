namespace AZ204_EntrAuth.HttpClient
{
	public interface IGraphHttpClient
	{
		// GET method
		Task<string> Get(string accessToken, string? restOfUrl = null);

		// POST method
		Task<string> Post(string accessToken, object data);

		// PUT method
		Task<string> Put(string accessToken, object data);

		// DELETE method
		Task<string> Delete(string accessToken);
	}
}
