using Lab3.CodeFirst.Models;
using Newtonsoft.Json;

namespace Lab3.CodeFirst.DTOs
{
    public class UserDto
    {
        [JsonProperty("id")]
        public Guid? Id { get; set; }
        [JsonProperty("firstName")]
        public string? FirstName { get; set; }
        [JsonProperty("middleName")]
        public string? MiddleName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("position")]
        public Position Position { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("groupId")]
        public Guid? GroupId { get; set; }
    }
}
