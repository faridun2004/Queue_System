using Queue_UI.Models;

namespace Queue_UI.Services
{
    public interface ITokenProvider
    {
        bool IsAuthenticated { get; }

        string GetAccessToken();  

        Task<(bool IsAuthenticated, string message)> LoginAsync(LoginModel loginModel);  
        void Logout();
        bool IsInRole(string role);
    }

}
