namespace Queue_UI.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public Guid GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? Role {  get; set; }= "User"; 
        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password));
        }
    }
}
