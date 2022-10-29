using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Model
{
    public interface IRepository
    {
        DataModel GetData();
        IEnumerable<Event> GetEvents(Func<Event, bool>? predicate = null);
        IEnumerable<Group> GetGroups(Func<Group, bool>? predicate = null);
        IEnumerable<User> GetUsers(Func<User, bool>? predicate = null);
    }
}
