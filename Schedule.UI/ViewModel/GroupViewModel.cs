using System.Collections.Generic;
using Schedule.Model;

namespace Schedule.UI.ViewModel;

public class GroupViewModel : ViewModelBase
{
    private int _id;
    private HashSet<UserViewModel> _students = new HashSet<UserViewModel>();
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

    public HashSet<UserViewModel> Students
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

    public void Add(UserViewModel user)
    {
        if (user.Position is not Position.Student || _students.Contains(user))
        {
            return;
        }

        _students.Add(user);
        OnPropertyChanged(nameof(Students));
    }

    public void Remove(UserViewModel user)
    {
        _students.Remove(user);
        OnPropertyChanged(nameof(Students));
    }

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not GroupViewModel group)
        {
            return false;
        }

        return group.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}