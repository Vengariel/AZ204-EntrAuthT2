using Flurl.Http;
namespace AZ204_EntrAuth
{
    internal class FlurlIt
    {
        const string GRAPH_URL = "https://graph.microsoft.com/v1.0/me";

        public static async Task<string> StalkThroughGraph(string accessToken)
        {

            try
            {
                return await GRAPH_URL.WithOAuthBearerToken(accessToken).GetStringAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return "error";
        }
    }
}
