using System.Security.Claims;

namespace Queue_WebApi.Auth
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? GetUserId()
        {
            var token = _httpContextAccessor.HttpContext?.User;
            var userIdClaim = token?.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (userIdClaim == null)
            {
                return null; 
            }
            return Guid.TryParse(userIdClaim, out var userId) ? userId : (Guid?)null;
        }
    }
}
