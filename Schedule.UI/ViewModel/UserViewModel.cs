using Schedule.Model;

namespace Schedule.UI.ViewModel;

public class UserViewModel : ViewModelBase
{
    private int _id;
    private string _firstName;
    private string _lastName;
    private string? _middleName;
    private Position _position;
    private string _email;
    private string _password;

    public int Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    public string? FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    public string? MiddleName
    {
        get => _middleName;
        set
        {
            _middleName = value;
            OnPropertyChanged(nameof(MiddleName));
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    public Position Position
    {
        get => _position;
        set
        {
            _position = value;
            OnPropertyChanged(nameof(Position));
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public override string ToString()
    {
        return $"#{Id} {LastName} {FirstName} {MiddleName}";
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not UserViewModel user)
        {
            return false;
        }

        return user.Id == Id;
    }
}