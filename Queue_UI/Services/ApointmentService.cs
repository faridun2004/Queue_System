using Queue_UI.Models;
using System.Net.Http.Json;

namespace Queue_UI.Services
{
    public class ApointmentService : IApointmentService
    {
        private readonly HttpClient _httpClient;
        private readonly UserService _userService;
        public ApointmentService(HttpClient httpClient, UserService userService)
        {
            _httpClient = httpClient;
            _userService = userService;
        }
        public async Task DeleteAppointment(Guid appointmentId)
        {
            var userId = await _userService.GetUserIdInToken();
            if (userId == null)
            {
                throw new Exception("User is not authenticated or token is invalid");
            }
            var url = $"api/appointment/user/{userId}/delete/{appointmentId}";
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
        public async Task<IEnumerable<AppointmentDro>> GetAllAppointments()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<AppointmentDro>>("api/appointment") ?? new List<AppointmentDro>();
        }
        public async Task<AppointmentDro> GetAppointmentById(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<AppointmentDro>($"api/appointment/{id}");
        }

        public async Task AddAppointment(AppointmentDro appointment)
        {
            await _httpClient.PostAsJsonAsync("api/appointment", appointment);
        }

        public async Task UpdateAppointment(Guid appointmentId, Guid hallId, DateTime startTime, DateTime endTime)
        {
            // Получаем userId через UserService
            var userId = await _userService.GetUserIdInToken();
            if (userId == null)
            {
                throw new Exception("User is not authenticated or token is invalid");
            }

            // Формируем URL с userId и appointmentId
            var url = $"api/appointment/user/{userId}/update/{appointmentId}";

            // Данные для обновления
            var updateData = new
            {
                hallId = hallId,
                startTime = startTime,
                endTime = endTime
            };

            // Отправляем запрос на обновление через HTTP PUT
            var response = await _httpClient.PutAsJsonAsync(url, updateData);

            // Проверка успешности запроса
            response.EnsureSuccessStatusCode();
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
