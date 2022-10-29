using System.Runtime.Serialization;

namespace Schedule.Model;

[DataContract]
public class User
{
    [DataMember]
    public int Id { get; set; }
    [DataMember]
    public string? FirstName { get; set; }
    [DataMember]
    public string LastName { get; set; }
    [DataMember]
    public string? MiddleName { get; set; }
    [DataMember]
    public Position Position { get; set; }
    [DataMember]
    public string Email { get; set; }
    [DataMember]
    public string Password { get; set; }
}

[DataContract]
public enum Position
{
    [EnumMember]
    Professor,
    [EnumMember]
    Assistant,
    [EnumMember]
    Student,
    [EnumMember]
    SystemAdministrator
}