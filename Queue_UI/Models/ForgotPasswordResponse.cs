namespace Queue_UI.Models
{
    public class ForgotPasswordResponse
    {
        public bool IsSuccess { get; set; }
        public string? NewPassword { get; set; }
        public string? ErrorMessage { get; set; }
    }

}
