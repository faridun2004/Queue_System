namespace Queue_UI.Services
{
    public interface IUserProvider
    {
        Guid? GetUserIdInToken();
    }
}
