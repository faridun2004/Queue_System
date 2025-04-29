using Queue_UI.Models;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net.Http;

namespace Queue_UI.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IServiceProvider _provider;
        private readonly ISyncLocalStorageService _localStorage;
        private static TokenInfo? token;

        public TokenProvider(IServiceProvider provider, ISyncLocalStorageService localStorage)
        {
            _provider = provider;
            _localStorage = localStorage;
            LoadTokenInfo();
        }
        
        public bool IsAuthenticated { get; private set; }

        private bool IsTokenExpired => token == null || token.ExpireTime < DateTimeOffset.Now;

        public string GetAccessToken()
        {
            if (IsTokenExpired && token != null)
            {
                // Асинхронное обновление токена
                RefreshTokenAsync(token.RefreshToken).ConfigureAwait(false);
            }

            return token?.AccessToken ?? string.Empty; // Возвращаем пустую строку, если токен не найден
        }

        private IHttpAPIProvider GetHttpAPIProvider() =>
            _provider.GetService<IHttpAPIProvider>();

        public async Task<(bool IsAuthenticated, string message)> LoginAsync(LoginModel loginModel)
        {
            // Запрос на сервер для получения токена
            (bool isSuccessStatusCode, TokenInfo tokenInfo, string message) = await GetHttpAPIProvider()
                .PostAsync<TokenInfo>($"Auth/Token?username={loginModel.Username}&password={loginModel.Password}");

            UpdateTokenInfo(isSuccessStatusCode, tokenInfo);

            return (isSuccessStatusCode, message);
        }

        public void Logout()
        {
            UpdateTokenInfo(false);
        }

        public void UpdateTokenInfo(bool successfullyAuthenticated, TokenInfo tokenInfo = null)
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

        private void LoadTokenInfo()
        {
            if (_localStorage.ContainKey(nameof(TokenInfo)))
            {
                token = _localStorage.GetItem<TokenInfo>(nameof(TokenInfo));
                IsAuthenticated = !IsTokenExpired;
            }
        }

        public bool IsInRole(string role)
        {
            var token = GetAccessToken();
            if (string.IsNullOrEmpty(token))
                return false;

            var claims = DecodeTokenClaims(token);
            Console.WriteLine("Decoded Claims: " + string.Join(", ", claims.Select(c => $"{c.Key}: {c.Value}")));

            return claims.ContainsKey(ClaimTypes.Role) && claims[ClaimTypes.Role] == role;
        }


        private Dictionary<string, string> DecodeTokenClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var claims = jsonToken?.Claims.ToDictionary(c => c.Type, c => c.Value);

            return claims ?? new Dictionary<string, string>(); 
        }

        private async Task RefreshTokenAsync(string refreshToken)
        {
            try
            {
                (bool isSuccessStatusCode, TokenInfo tokenInfo, string message) = await GetHttpAPIProvider()
                    .PostAsync<TokenInfo>($"Auth/RefreshToken?refreshToken={refreshToken}");

                UpdateTokenInfo(isSuccessStatusCode, tokenInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing token: {ex.Message}");
                UpdateTokenInfo(false);
            }
        }
    }
}
