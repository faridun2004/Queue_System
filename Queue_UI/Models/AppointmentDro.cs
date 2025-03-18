namespace Queue_UI.Models
{
    public class AppointmentDro
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } 
        public string GroupName { get; set; } 
        public string HallName { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid UserId { get; set; }
        public Guid HallId { get; set; }
        public bool IsExpired => EndTime < DateTime.UtcNow;
    }
}
