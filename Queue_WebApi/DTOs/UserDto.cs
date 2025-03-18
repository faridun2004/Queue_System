namespace Queue_WebApi.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public Guid GroupId { get; set; }
        public string? GroupName { get; set; }
        
        public string? Role { get; set; }
    }
}
