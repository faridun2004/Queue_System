namespace Appointment_Hall.Models
{
    public class Hall: BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Reservation> Reservations { get; set; }  // Резервирования на этот зал
    }

}
