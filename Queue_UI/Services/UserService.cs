using System.Text.Json;

namespace Queue_UI.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenProvider _tokenProvider;
        public Guid? UserId { get; private set; }

        public UserService(HttpClient httpClient, ITokenProvider tokenProvider)
        {
            _httpClient = httpClient;
            _tokenProvider = tokenProvider;
        }
        public async Task DeleteAppointment(Guid appointmentId)
        {
            var id = await GetUserIdInToken();
            if (id == null)
            {
                throw new Exception("User is not authenticated or token is invalid");
            }
            var url = $"api/user/{id}";
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
        public async Task<Guid?> GetUserIdInToken()
        {
            try
            {
                var accessToken = _tokenProvider.GetAccessToken();
                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new ArgumentNullException();
                }

                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await _httpClient.GetAsync("GetUserIdInJwt");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var userId = JsonSerializer.Deserialize<Guid>(content);
                    return userId;
                }
                else
                {
                    throw new Exception("401! Unauthorized");
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

}
