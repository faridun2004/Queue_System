using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Queue_WebApi.DTOs;
using Queue_WebApi.Models;
using Queue_WebApi.Persistence.DataBase;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Queue_WebApi.Auth
{
    public class AuthService
    {
        private readonly QueueDbContext _context;

        public AuthService(QueueDbContext context)
        {
            _context = context;
        }
        public async Task<TokenInfo> Login(string username, string password)
        {
            var user = await _context.Users.Where(z=>z.IsDeleted == false).SingleOrDefaultAsync(x => x.Username == username);
            if (user == null || !VerifyPassword(password, user.Password))
                throw new ArgumentException("Invalid username or password.");

            return await GenerateJWT(user);
        }
        public async Task<TokenInfo> RefreshToken(string refreshToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.RefreshToken == refreshToken);
            if (user == null)
                throw new ArgumentException("Invalid refresh token.");
            return await GenerateJWT(user);
        }
        private async Task<TokenInfo> GenerateJWT(User user)
        {
            if (user.IsBlocked)
                throw new ArgumentException("User does not have access to login.");
            var userRoles = new string[] { user.Role ?? "User" };
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role ?? "User")
            };
            var expireTime = DateTime.UtcNow.AddMinutes(AuthOptions.LIFETIME);
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: expireTime,
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            var refreshToken = Guid.NewGuid().ToString();
            user.RefreshToken = refreshToken;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return new TokenInfo { AccessToken = accessToken, RefreshToken = refreshToken, ExpireTime = expireTime };
        }
        private bool VerifyPassword(string inputPassword, string storedPasswordHash)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedPasswordHash);
        }
    }

}

