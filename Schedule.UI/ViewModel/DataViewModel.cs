using System.Collections.ObjectModel;

namespace Schedule.UI.ViewModel;

public class DataViewModel : ViewModelBase
{
    public ObservableCollection<UserViewModel> Users { get; set; }
    public ObservableCollection<EventViewModel> Events { get; set; }
    public ObservableCollection<GroupViewModel> Groups { get; set; }
}