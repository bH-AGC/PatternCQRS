using System.Text.Json.Serialization;

namespace WriterAPI.Models;

public class UserResponse
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    public string Name { get; set; }
}