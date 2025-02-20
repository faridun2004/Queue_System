namespace Queue_WebApi.Models
{
    public class Apointment
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int QueueId { get; set; } 
        public Queue Queue { get; set; }
    }

}

