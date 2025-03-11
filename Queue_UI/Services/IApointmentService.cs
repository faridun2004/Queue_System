using Queue_UI.Models;

namespace Queue_UI.Services
{
    public interface IApointmentService
    {
        Task<IEnumerable<AppointmentDro>> GetAllAppointments();
        Task<AppointmentDro> GetAppointmentById(Guid id);
        Task AddAppointment(AppointmentDro appointment);
        Task UpdateAppointment(Guid appointmentId, Guid hallId, DateTime startTime, DateTime endTimeAppointmentDro);
        Task DeleteAppointment(Guid id);
    }
}
