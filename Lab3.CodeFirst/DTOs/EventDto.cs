using Lab3.CodeFirst.Models;
using Newtonsoft.Json;

namespace Lab3.CodeFirst.DTOs
{
    public class EventDto
    {
        [JsonProperty("id")]
        public Guid? Id { get; set; }
        [JsonProperty("order")]
        public int Order { get; set; }
        [JsonProperty("professorId")]
        public Guid ProfessorId { get; set; }
        [JsonProperty("groupId")]
        public Guid GroupId { get; set; }
        [JsonProperty("type")]
        public EventType Type { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("day")]
        public DayOfWeek DayOfWeek { get; set; }
    }
}
