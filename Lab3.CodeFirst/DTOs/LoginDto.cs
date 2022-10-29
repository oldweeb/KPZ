using Newtonsoft.Json;

namespace Lab3.CodeFirst.DTOs;

public class LoginDto
{
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("password")]
    public string Password { get; set; }
}