namespace Queue_WebApi.DTOs
{
    public class UpdateAppointmentDto
    {
        public Guid HallId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
