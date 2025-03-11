namespace Queue_UI.Models
{
    public class UserDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public Guid GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? Role { get; set; }
        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Role) || string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password));
        }
    }
}
