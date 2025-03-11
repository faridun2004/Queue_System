namespace Queue_WebApi.Models
{
    public class Group: BaseEntity
    {
        public string? Name { get; set; }
        public List<User>? Users { get; set; }
    }
}
