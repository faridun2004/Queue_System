namespace Queue_WebApi.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public string HallName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }= DateTime.UtcNow;
        public DateTime EndTime { get; set; }=DateTime.UtcNow;
        
    }
}
