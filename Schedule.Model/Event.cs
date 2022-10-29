using System.Runtime.Serialization;

namespace Schedule.Model;

[DataContract]
public class Event
{
    [DataMember]
    public int Id { get; set; }
    [DataMember]
    public int Order { get; set; }
    [DataMember]
    public User Professor { get; set; }
    [DataMember]
    public Group Group { get; set; }
    [DataMember]
    public EventType Type { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public DayOfWeek DayOfWeek { get; set; }
}

public enum EventType
{
    Lab,
    Lecture,
    Practice
}