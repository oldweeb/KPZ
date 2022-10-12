using System.Collections.Generic;

namespace Schedule.UI.ViewModel;

public class GroupViewModel : ViewModelBase
{
    private int _id;
    private List<UserViewModel> _students;
    private string _name;
    public int Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    public List<UserViewModel> Students
    {
        get => _students;
        set
        {
            _students = value;
            OnPropertyChanged(nameof(Students));
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }
}