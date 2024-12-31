namespace GameBook.Helpers.SessionHelper
{
    public interface ISessionService
    {
        void Set<T>(string key, T value);
        T? Get<T>(string key);
        void Clear(string key);
    }
}
