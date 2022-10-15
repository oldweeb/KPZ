using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Schedule.Model;
using Schedule.UI.Commands;
using Schedule.UI.Views;

namespace Schedule.UI.ViewModel;

public class LoginViewModel : ViewModelBase
{
    private readonly IRepository _repository;
    private ICommand? _login;
    private string? _email;
    private string? _password;

    public LoginViewModel(IRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public string? Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public string? Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public ICommand LoginCommand
    {
        get
        {
            _login ??= new RelayCommand(
                _ => HandleLogin(),
                _ => CanLogin());

            return _login;
        }
    }

    public LoginWindow CurrentWindow { get; set; }

    private bool CanLogin()
    {
        return !String.IsNullOrEmpty(_email) && !String.IsNullOrEmpty(_password);
    }

    private void HandleLogin()
    {
        User? user = _repository.GetUsers().FirstOrDefault(u => u.Email.ToLower() == _email.ToLower() && u.Password == _password);
        if (user is null)
        {
            MessageBox.Show("Ти нонамес");
            return;
        }

        IServiceProvider services = App.Services!;
        var mapper = services.GetRequiredService<IMapper>();
        var userVM = mapper.Map<UserViewModel>(user);

        Window window;

        if (user.Position == Position.Student)
        {
            window = services.GetRequiredService<StudentWindow>();
            var studentVM = services.GetRequiredService<StudentViewModel>();
            studentVM.Student = userVM;
            studentVM.CurrentWindow = window;
            window.DataContext = studentVM;
        }
        else
        {
            window = services.GetRequiredService<ProfessorWindow>();
            var professorVM = services.GetRequiredService<ProfessorViewModel>();
            professorVM.Professor = userVM;
            professorVM.CurrentWindow = window;
            window.DataContext = professorVM;
        }

        window.Show();
        CurrentWindow.Close();
    }
}