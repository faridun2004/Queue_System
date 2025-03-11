using Queue_UI.Models;

namespace Queue_UI.Services
{
    public interface ITokenProvider
    {
        bool IsAuthenticated { get; }

        string GetAccessToken();  // Получение токена

        Task<(bool IsAuthenticated, string message)> LoginAsync(LoginModel loginModel);  // Логин

        void Logout();  // Логаут
    }


}
