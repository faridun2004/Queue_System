namespace Queue_UI.Models
{
    public class UpdateAppointmentCommand
    {
        public Guid AppointmentId { get; set; }
        public Guid HallId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
