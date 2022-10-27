namespace Lab3.DbFirst.Models;

public partial class Group
{
    public Group()
    {
        Events = new HashSet<Event>();
        Students = new HashSet<User>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Event> Events { get; set; }
    public virtual ICollection<User> Students { get; set; }
}