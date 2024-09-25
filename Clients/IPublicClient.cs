
namespace AZ204_EntrAuth.Clients
{
    public interface IPublicClient
    {
        Task Build_Process(AppSettings appSettings, string[] scopes);
    }
}