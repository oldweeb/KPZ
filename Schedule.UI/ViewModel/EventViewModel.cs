using System;
using Schedule.Model;

namespace Schedule.UI.ViewModel;

public class EventViewModel : ViewModelBase
{
    private int _id;
    private int _order;
    private string _name;
    private UserViewModel _user;
    private EventType _type;
    private GroupViewModel _group;
    private DayOfWeek _dayOfWeek;

    public int Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    public int Order
    {
        get => _order;
        set
        {
            _order = value;
            OnPropertyChanged(nameof(Order));
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

    public UserViewModel User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged(nameof(User));
        }
    }

    public EventType Type
    {
        get => _type;
        set
        {
            _type = value;
            OnPropertyChanged(nameof(Type));
        }
    }

    public DayOfWeek DayOfWeek
    {
        get => _dayOfWeek;
        set
        {
            _dayOfWeek = value;
            OnPropertyChanged(nameof(DayOfWeek));
        }
    }

    public GroupViewModel Group
    {
        get => _group;
        set
        {
            _group = value;
            OnPropertyChanged(nameof(Group));
        }
    }
}