using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Schedule.Model;

namespace Schedule.UI.Repositories;

internal class HardCodedRepository : IRepository
{
    [JsonProperty("events")]
    private readonly List<Event> _events;
    [JsonProperty("groups")]
    private readonly List<Group> _groups;
    [JsonProperty("users")]
    private readonly List<User> _users;

    public HardCodedRepository()
    {
        var json = File.ReadAllText(GlobalConfiguration.DataPath);
        var data = JsonConvert.DeserializeObject<DataModel>(json);
        _events = new List<Event>(data.Events);
        _groups = new List<Group>(data.Groups);
        _users = new List<User>(data.Users);
    }

    public DataModel GetData()
    {
        return new DataModel()
        {
            Events = _events,
            Groups = _groups,
            Users = _users
        };
    }

    public IEnumerable<Event> GetEvents(Func<Event, bool>? predicate = null)
    {
        return predicate != null ? _events.Where(predicate) : _events;
    }

    public IEnumerable<Group> GetGroups(Func<Group, bool>? predicate = null)
    {
        return predicate != null ? _groups.Where(predicate) : _groups;
    }

    public IEnumerable<User> GetUsers(Func<User, bool>? predicate = null)
    {
        return predicate != null ? _users.Where(predicate) : _users;
    }

    public IEnumerable<User> GetStudents()
    {
        return _users.Where(user => user.Position == Position.Student);
    }

    public IEnumerable<User> GetTeachers()
    {
        return _users.Where(user => user.Position != Position.Student);
    }
}