using Queue_UI.Models;
using System.Net.Http.Json;

namespace Queue_UI.Services
{
    public class ApointmentService : IApointmentService
    {
        private readonly HttpClient _httpClient;
        public ApointmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<AppointmentDro>> GetAllAppointments()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<AppointmentDro>>("api/appointment") ?? new List<AppointmentDro>();
        }
        public async Task<CreateAppointmentDto> GetAppointmentById(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<CreateAppointmentDto>($"api/appointment/{id}");
        }

        public async Task AddAppointment(CreateAppointmentDto appointment)
        {
            await _httpClient.PostAsJsonAsync("api/appointment", appointment);
        }

        public async Task UpdateAppointment(CreateAppointmentDto appointment)
        {
            await _httpClient.PutAsJsonAsync($"api/appointment/{appointment.Id}", appointment);
        }
        //public async Task<QueueDto> GetQueueByHallId(Guid hallId)
        //{
        //    return await _httpClient.GetFromJsonAsync<QueueDto>($"api/queue/{hallId}");
        //}
        //public async Task<QueueDto> CreateQueue(QueueDto queue)
        //{
        //    var response = await _httpClient.PostAsJsonAsync("api/queue", queue);
        //    return await response.Content.ReadFromJsonAsync<QueueDto>();
        //}
        //public async Task UpdateQueue(QueueDto queue)
        //{
        //    await _httpClient.PutAsJsonAsync("api/queue", queue);
        //}
        //public async Task DeleteQueue(Guid id)
        //{
        //    await _httpClient.DeleteAsync($"api/queue/{id}");

    }
}
