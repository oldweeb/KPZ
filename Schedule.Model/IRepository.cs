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
        IEnumerable<Event> GetEvents();
        IEnumerable<Group> GetGroups();
        IEnumerable<User> GetUsers();
    }
}
