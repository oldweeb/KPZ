using System.Runtime.Serialization;

namespace Schedule.Model;

[DataContract]
public class DataModel
{
    [DataMember]
    public IEnumerable<User> Users { get; set; }
    [DataMember]
    public IEnumerable<Event> Events { get; set; }
    [DataMember]
    public IEnumerable<Group> Groups { get; set; }
}