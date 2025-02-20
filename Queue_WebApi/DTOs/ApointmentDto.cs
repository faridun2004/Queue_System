namespace Queue_WebApi.DTOs
{
    public class ApointmentDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
    }
}
