namespace Queue_WebApi.Models
{
    public class Queue
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public List<Apointment> Apointments { get; set; } = new();
    }
}
