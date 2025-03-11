namespace Queue_UI.Models
{
    public class QueueDto
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public string HallName { get; set; }
        public int Number { get; set; }
        public List<AppointmentDro> Apointments { get; set; }
    }
}
