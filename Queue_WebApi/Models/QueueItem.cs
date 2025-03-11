namespace Queue_WebApi.Models
{
    public class QueueItem: BaseEntity
    {
        public Guid HallId { get; set; }
        public Hall? Hall { get; set; }
        public Guid UserId { get; set; }  
        public User? User { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
