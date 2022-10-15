using System.Runtime.Serialization;

namespace Schedule.Model;

[DataContract]
public class Group
{
    [DataMember]
    public int Id { get; set; }
    [DataMember]
    public HashSet<User> Students { get; set; }
    [DataMember]
    public string Name { get; set; }
}