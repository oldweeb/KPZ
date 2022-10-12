using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                _ => !String.IsNullOrEmpty(_email) && !String.IsNullOrEmpty(_password));

            return _login;
        }
    }

    public LoginWindow CurrentWindow { get; set; }

    private void HandleLogin()
    {
        User? user = _repository.GetUsers().FirstOrDefault(u => u.Email.ToLower() == _email.ToLower() && u.Password == _password);
        if (user is null)
        {
            MessageBox.Show("Ти нонамес");
            return;
        }

        if (user.Position == Position.Student)
        {
            var mapper = App.Services!.GetRequiredService<IMapper>();
            var studentWindow = App.Services!.GetRequiredService<StudentWindow>();
            var userVM = mapper.Map<UserViewModel>(user);
            studentWindow.DataContext = userVM;
            studentWindow.Show();
            CurrentWindow.Close();
            return;
        }
    }
}