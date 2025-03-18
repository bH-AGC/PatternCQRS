using System.Data;
using Microsoft.Data.SqlClient;
using ReaderAPI.Models;

namespace ReaderAPI.Services
{
    public class MessageService
    {
        private readonly string _messagesConnectionString;

        public MessageService(string messagesConnectionString)
        {
            _messagesConnectionString = messagesConnectionString;
        }
        
        // Méthode pour récupérer tous les messages
        public List<Message> GetAllMessages()
        {
            SqlConnection connection = new SqlConnection(_messagesConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("GetAllMessages", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            SqlDataReader reader = command.ExecuteReader();
            List<Message> messages = new List<Message>();
            while (reader.Read())
            {
                messages.Add(new Message
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                    Content = reader.GetString(reader.GetOrdinal("Content"))
                });
            }
            return messages;
        }
    }
}
