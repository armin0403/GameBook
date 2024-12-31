using System.Text.Json;

namespace GameBook.Helpers.SessionHelper
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void Clear(string key)
        {
            _httpContextAccessor.HttpContext?.Session.Remove(key);
        }

        public T? Get<T>(string key)
        {
            var jsonString = _httpContextAccessor.HttpContext?.Session.GetString(key);
            return jsonString != null ? JsonSerializer.Deserialize<T>(jsonString) : default;
        }

        public void Set<T>(string key, T value)
        {
            var jsonString = JsonSerializer.Serialize(value);
            _httpContextAccessor.HttpContext?.Session.SetString(key, jsonString);
        }
    }
}
