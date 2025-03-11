namespace Appointment_Hall.Models
{
    public class Queue: BaseEntity
    {
        public Guid Id { get; set; }
        public Guid HallId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime RequestedStartTime { get; set; }
        public DateTime RequestedEndTime { get; set; }
        public Client Client { get; set; }
    }

}
