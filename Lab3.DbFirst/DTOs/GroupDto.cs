using Newtonsoft.Json;

namespace Lab3.DbFirst.DTOs;

public class GroupDto
{
    [JsonProperty("id")]
    public Guid? Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
}