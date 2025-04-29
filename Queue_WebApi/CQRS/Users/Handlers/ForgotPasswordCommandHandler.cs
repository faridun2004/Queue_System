using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.CQRS.Users.Commands;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Users.Handlers
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, string>
    {
        private readonly QueueDbContext _dbContext;

        public ForgotPasswordCommandHandler(QueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.KeyWord == request.KeyWord && u.Username==request.UserName && !u.IsDeleted);
            if (user == null)
            {
                throw new ArgumentException("Неверная имя или ключевие слово");
            }
            string newPassword = GenerateNewPassword();
            user.Password = HashPassword(newPassword);
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return newPassword; 
        }
        private string GenerateNewPassword()
        {
            var random = new Random();
            var passwordLength = 12;
            var passwordChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            var newPassword = new char[passwordLength];
            for (int i = 0; i < passwordLength; i++)
            {
                newPassword[i] = passwordChars[random.Next(passwordChars.Length)];
            }
            return new string(newPassword);
        }
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password); 
        }
    }
}
