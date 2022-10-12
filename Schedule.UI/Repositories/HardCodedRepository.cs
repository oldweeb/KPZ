using System.Collections.Generic;
using System.Linq;
using Schedule.Model;

namespace Schedule.UI.Repositories;

internal class HardCodedRepository : IRepository
{
    private readonly List<Event> _events;
    private readonly List<Group> _groups;
    private readonly List<User> _users;

    public HardCodedRepository()
    {
        _users = new List<User>()
        {
            new User()
            {
                Email = "maxvill2016@gmail.com",
                FirstName = "Maksym",
                LastName = "Pavliv",
                Password = "Unga bunga",
                Position = Position.Student,
                Id = 1
            },
            new User()
            {
                Email = "maksym.pavliv.pz.2020@lpnu.ua",
                LastName = "Tratatov",
                Id = 2,
                Password = "ungabunga 2",
                Position = Position.Student,
            },
            new User()
            {
                Email = "test@test.com",
                LastName = "Test",
                MiddleName = "Professorovych",
                Position = Position.Professor,
                Id = 3,
                Password = "lolkektratata"
            },
            new User()
            {
                Email = "test@test2.com",
                Id = 4,
                LastName = "Bob",
                FirstName = "Gubka",
                Password = "12345678",
                Position = Position.Assistant
            },
            new User()
            {
                Id = 5,
                Position = Position.Student,
                FirstName = "Mykhailo",
                LastName = "Vovchanyk",
                Email = "trata@test.com",
                Password = "123qwe123"
            }
        };

        _groups = new List<Group>()
        {
            new Group()
            {
                Id = 1,
                Name = "PZ-32",
                Students = new List<User>()
                {
                    _users[4],
                    _users[0]
                }
            },
            new Group()
            {
                Id = 2,
                Name = "PZ-41",
                Students = new List<User>()
                {
                    _users[1]
                }
            }
        };

        _events = new List<Event>()
        {
            new Event()
            {
                Group = _groups[0],
                Id = 1,
                Name = "KPZ",
                Order = 2,
                Professor = _users[2],
                Type = EventType.Lecture
            },
            new Event()
            {
                Group = _groups[0],
                Id = 2,
                Name = "PE",
                Order = 5,
                Professor = _users[3],
                Type = EventType.Practice
            },
            new Event()
            {
                Group = _groups[0],
                Id = 3,
                Name = "C#",
                Order = 3,
                Professor = _users[2],
                Type = EventType.Lab
            },
            new Event()
            {
                Group = _groups[1],
                Id = 4,
                Name = "Operation Research",
                Order = 2,
                Professor = _users[3],
                Type = EventType.Practice
            }
        };
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

    public IEnumerable<Event> GetEvents()
    {
        return _events;
    }

    public IEnumerable<Group> GetGroups()
    {
        return _groups;
    }

    public IEnumerable<User> GetUsers()
    {
        return _users;
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