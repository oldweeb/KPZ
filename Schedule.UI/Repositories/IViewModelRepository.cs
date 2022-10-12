using System.Collections.Generic;
using Schedule.UI.ViewModel;

namespace Schedule.UI.Repositories;

public interface IViewModelRepository
{
    DataViewModel GetData();
    IEnumerable<EventViewModel> GetEvents();
    IEnumerable<GroupViewModel> GetGroups();
    IEnumerable<UserViewModel> GetUsers();
}