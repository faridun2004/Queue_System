using Microsoft.IdentityModel.Tokens;
using Queue_WebApi.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using Queue_WebApi.Models;
using Queue_WebApi.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;

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
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username && x.Password == password);
            return await GeneratedJWT(user);
        }

        public async Task<TokenInfo> RefreshToken(string refreshToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.RefreshToken == refreshToken);
            return await GeneratedJWT(user);
        }

        async Task<TokenInfo> GeneratedJWT(User user)
        {
            if (user is null)
                throw new ArgumentException("Invalid username or password.");

            if (user.IsBlocked)
                throw new ArgumentException("User does not have access to login.");

            var userRoles = new string[] { user.Role ?? "User" };
            var claims = new List<Claim> {
            new Claim("Id", user.Id.ToString()),  // Сохраняем Id как строку
            new Claim(ClaimTypes.Name, user.Name)
        };

            foreach (var userRole in userRoles)
                claims.Add(new Claim(ClaimTypes.Role, userRole));

            var expireTime = DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME));
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            var refreshToken = Guid.NewGuid().ToString();
            user.RefreshToken = refreshToken;

            _context.Update(user);
            await _context.SaveChangesAsync();

            return new TokenInfo { AccessToken = accessToken, RefreshToken = refreshToken, ExpireTime = expireTime };
        }
    }

}

