namespace Queue_WebApi.Exceptions
{
    public class AppointmentNotFoundException:Exception
    {
        public AppointmentNotFoundException(Guid id)
            : base($"Appointment with id {id} not found") { }
        public AppointmentNotFoundException(string message)
            : base(message) { }
        public AppointmentNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
