namespace Queue_WebApi.DTOs
{
    public class ForgotPasswordRequests
    {
        public string? UserName { get; set; }
        public string KeyWord { get; set; } = string.Empty;
    }
}
