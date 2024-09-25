
namespace AZ204_EntrAuth.Clients
{
    public interface IConfidentialClient
    {
        Task Build_Process(AppSettings appSettings, string[] scopes);
    }
}