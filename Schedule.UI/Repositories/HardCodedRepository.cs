using System;
using System.Collections.Generic;
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
        //_users = new List<User>()
        //{
        //    new User()
        //    {
        //        Email = "maxvill2016@gmail.com",
        //        FirstName = "Maksym",
        //        LastName = "Pavliv",
        //        Password = "Unga bunga",
        //        Position = Position.Student,
        //        Id = 1
        //    },
        //    new User()
        //    {
        //        Email = "maksym.pavliv.pz.2020@lpnu.ua",
        //        LastName = "Tratatov",
        //        Id = 2,
        //        Password = "ungabunga 2",
        //        Position = Position.Student,
        //    },
        //    new User()
        //    {
        //        Email = "test@test.com",
        //        LastName = "Test",
        //        MiddleName = "Professorovych",
        //        Position = Position.Professor,
        //        Id = 3,
        //        Password = "lolkektratata"
        //    },
        //    new User()
        //    {
        //        Email = "test@test2.com",
        //        Id = 4,
        //        LastName = "Bob",
        //        FirstName = "Gubka",
        //        Password = "12345678",
        //        Position = Position.Assistant
        //    },
        //    new User()
        //    {
        //        Id = 5,
        //        Position = Position.Student,
        //        FirstName = "Mykhailo",
        //        LastName = "Vovchanyk",
        //        Email = "trata@test.com",
        //        Password = "123qwe123"
        //    },
        //    new User()
        //    {
        //        Id = 6,
        //        Position = Position.SystemAdministrator,
        //        LastName = "admin",
        //        Email = "admin",
        //        Password = "admin"
        //    }
        //};

        //_groups = new List<Group>()
        //{
        //    new Group()
        //    {
        //        Id = 1,
        //        Name = "PZ-32",
        //        Students = new HashSet<User>()
        //        {
        //            _users[4],
        //            _users[0]
        //        }
        //    },
        //    new Group()
        //    {
        //        Id = 2,
        //        Name = "PZ-41",
        //        Students = new HashSet<User>()
        //        {
        //            _users[1]
        //        }
        //    }
        //};

        //_events = new List<Event>()
        //{
        //    new Event()
        //    {
        //        Group = _groups[0],
        //        Id = 1,
        //        Name = "KPZ",
        //        Order = 2,
        //        Professor = _users[2],
        //        Type = EventType.Lecture,
        //        DayOfWeek = DayOfWeek.Monday
        //    },
        //    new Event()
        //    {
        //        Group = _groups[0],
        //        Id = 2,
        //        Name = "PE",
        //        Order = 5,
        //        Professor = _users[3],
        //        Type = EventType.Practice,
        //        DayOfWeek = DayOfWeek.Wednesday
        //    },
        //    new Event()
        //    {
        //        Group = _groups[0],
        //        Id = 3,
        //        Name = "C#",
        //        Order = 3,
        //        Professor = _users[2],
        //        Type = EventType.Lab,
        //        DayOfWeek = DayOfWeek.Wednesday
        //    },
        //    new Event()
        //    {
        //        Group = _groups[1],
        //        Id = 4,
        //        Name = "Operation Research",
        //        Order = 2,
        //        Professor = _users[3],
        //        Type = EventType.Practice,
        //        DayOfWeek = DayOfWeek.Thursday
        //    }
        //};
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