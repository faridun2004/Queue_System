namespace Appointment_Hall.Use_Cases.DTOs
{
    public class ReservationDto
    {
        public string HallName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
