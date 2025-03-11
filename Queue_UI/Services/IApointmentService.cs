using Queue_UI.Models;

namespace Queue_UI.Services
{
    public interface IApointmentService
    {
        Task<IEnumerable<AppointmentDro>> GetAllAppointments();
        Task<CreateAppointmentDto> GetAppointmentById(Guid id);
        Task AddAppointment(CreateAppointmentDto appointment);
        Task UpdateAppointment(CreateAppointmentDto appointment);
        //Task<QueueDto> GetQueueByHallId(int hallId);
        //Task<QueueDto> GetByUserId(int userId);
    }
}
