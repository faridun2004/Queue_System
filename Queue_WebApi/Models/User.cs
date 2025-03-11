using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Queue_WebApi.Models
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public Guid GroupId { get; set; }
        public Group? Group { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }  
        public string? RefreshToken { get; set; }
        public string? Role { get; set; }
        [JsonIgnore]
        public bool IsBlocked { get; }
        public bool IsDeleted { get; set; }
    }
}
