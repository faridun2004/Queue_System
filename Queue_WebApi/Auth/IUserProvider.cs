namespace Queue_WebApi.Auth
{
    public interface IUserProvider
    {
        Guid? GetUserId();
    }
}
