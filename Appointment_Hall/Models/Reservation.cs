namespace Appointment_Hall.Models
{
    public class Reservation: BaseEntity
    {

        public Guid HallId { get; set; }
        public Hall? Hall { get; set; }

        public Guid ClientId { get; set; }
        public Client? Client { get; set; } 

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

}
