using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Schedule.UI.Commands;
using Schedule.UI.Views;

namespace Schedule.UI.ViewModel
{
    public class LogoutViewModelBase : ViewModelBase
    {
        protected ICommand? _logoutCommand;

        public virtual ICommand Logout
        {
            get
            {
                _logoutCommand ??= new RelayCommand(_ => HandleLogout());
                return _logoutCommand;
            }
        }

        public virtual Window CurrentWindow { get; set; }

        private void HandleLogout()
        {
            IServiceProvider services = App.Services!;
            var loginWindow = services.GetRequiredService<LoginWindow>();
            var loginVM = services.GetRequiredService<LoginViewModel>();
            loginWindow.DataContext = loginVM;
            loginVM.CurrentWindow = loginWindow;
            loginWindow.Show();
            CurrentWindow.Close();
        }
    }
}
