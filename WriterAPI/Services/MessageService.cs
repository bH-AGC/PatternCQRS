using System.Data;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Text.Json;
using WriterAPI.Models;

namespace WriterAPI.Services
{
    public class MessageService
    {
        private readonly string _messagesConnectionString;
        private readonly HttpClient _httpClient;

        public MessageService(string messagesConnectionString, HttpClient httpClient)
        {
            _messagesConnectionString = messagesConnectionString;
            _httpClient = httpClient;
        }

        public async Task<int> AddMessage(string username, string content)
        {
            int userId = await CreateUserAsync(username);;
            if (userId == 0)
            {
                return 0;
            }
            
            SqlConnection connection = new SqlConnection(_messagesConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("AddMessage", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@Content", content);

            int result = command.ExecuteNonQuery();
            return result;
        }
        
        private async Task<int> CreateUserAsync(string username)
        {
            var content = new StringContent(JsonSerializer.Serialize(username), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7098/api/users", content);

            if (response.IsSuccessStatusCode)
            {
                var contentResponse = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<UserResponse>(contentResponse);
                return user?.UserId ?? 0;
            }
    
            return 0;
        }
    }
}
