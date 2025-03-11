namespace Queue_UI.Models
{
    public class CreateAppointmentDto
    {
        public Guid Id { get; set; }
        public Guid HallId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
