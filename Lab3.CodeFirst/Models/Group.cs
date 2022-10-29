namespace Lab3.CodeFirst.Models;

public class Group
{
    public Guid Id { get; set; }
    public ICollection<User> Students { get; set; }
    public string Name { get; set; }
    public GroupType? Type { get; set; }
}

public enum GroupType
{
    Online,
    Offline
}