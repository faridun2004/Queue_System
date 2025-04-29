using MediatR;

namespace Queue_WebApi.CQRS.Users.Commands
{
    public class ForgotPasswordCommand : IRequest<string>
    {
        public string KeyWord { get; set; }
        public string UserName { get; set; }    

        public ForgotPasswordCommand(string keyWord, string userName)
        {
            KeyWord = keyWord;
            UserName = userName;
        }
    }


}
