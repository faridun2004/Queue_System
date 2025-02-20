namespace Queue_WebApi.Models
{
    public abstract class User
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public string PhoneNumber { get; set; }
    }
}
