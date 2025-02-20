namespace Queue_WebApi.Models
{
    public class Hall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address {  get; set; }
        public string PhoneNumber { get; set; }
        public int workerId {  get; set; }
        public Worker Worker { get; set; }
    }
}
