using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class MessageService
    {
        private readonly HttpClient _httpClient;

        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendMessageAsync(string username, string content)
        {
            var message = new { UserName = username, Content = content };
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7065/api/messages", message);

            return response.IsSuccessStatusCode;
        }
    }
}