using Queue_UI.Models;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
namespace Queue_UI.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IServiceProvider _provider;
        private readonly ISyncLocalStorageService _localStorage;
        private static TokenInfo token;

        public TokenProvider(IServiceProvider provider, ISyncLocalStorageService localStorage)
        {
            _provider = provider;
            _localStorage = localStorage;
            LoadTokenInfo();
        }

        public bool IsAuthenticated { get; private set; }

        private bool IsTokenExpired => token == null || token.ExpireTime < DateTimeOffset.Now;

        // Получение токена
        public string GetAccessToken()
        {
            if (IsTokenExpired && token != null)
            {
                RefreshTokenAsync(token.RefreshToken).GetAwaiter().GetResult(); // Асинхронное обновление токена
            }

            return token?.AccessToken;
        }

        private IHttpAPIProvider GetHttpAPIProvider() =>
            _provider.GetService<IHttpAPIProvider>();

        // Вход пользователя
        public async Task<(bool IsAuthenticated, string message)> LoginAsync(LoginModel loginModel)
        {
            (bool isSuccessStatusCode, TokenInfo tokenInfo, string message) = await GetHttpAPIProvider()
                .PostAsync<TokenInfo>($"Auth/Token?username={loginModel.Username}&password={loginModel.Password}");

            UpdateTokenInfo(isSuccessStatusCode, tokenInfo);

            return (isSuccessStatusCode, message);
        }

        // Выход пользователя
        public void Logout()
        {
            UpdateTokenInfo(false);
        }

        // Обновление информации о токене
        private void UpdateTokenInfo(bool successfullyAuthenticated, TokenInfo tokenInfo = null)
        {
            if (successfullyAuthenticated)
            {
                IsAuthenticated = true;
                token = tokenInfo;

                _localStorage.SetItem(nameof(TokenInfo), tokenInfo);
            }
            else
            {
                IsAuthenticated = false;
                token = null;

                _localStorage.RemoveItem(nameof(TokenInfo));
            }
        }

        // Загрузка информации о токене
        private void LoadTokenInfo()
        {
            if (_localStorage.ContainKey(nameof(TokenInfo)))
            {
                token = _localStorage.GetItem<TokenInfo>(nameof(TokenInfo));
                IsAuthenticated = !IsTokenExpired;
            }
        }

        // Обновление токена с использованием refresh токена
        private async Task RefreshTokenAsync(string refreshToken)
        {
            (bool isSuccessStatusCode, TokenInfo tokenInfo, string message) = await GetHttpAPIProvider()
                .PostAsync<TokenInfo>($"Auth/RefreshToken?refreshToken={refreshToken}");

            UpdateTokenInfo(isSuccessStatusCode, tokenInfo);
        }
    }

}
