using System;
using System.IO;
using System.Windows;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schedule.Model;
using Schedule.UI.Mapper;
using Schedule.UI.Repositories;
using Schedule.UI.ViewModel;
using Schedule.UI.Views;

namespace Schedule.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IHost? Host { get; private set; }
    public static IServiceProvider? Services => Host?.Services; 

    public App()
    {
        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                var json = File.ReadAllText(GlobalConfiguration.DataPath);
                services.AddTransient<IRepository, HardCodedRepository>();
                services.AddSingleton<IMapper>(Mapping.Create());
                services.AddTransient<LoginWindow>();
                services.AddTransient<LoginViewModel>();
                services.AddTransient<StudentWindow>();
                services.AddTransient<StudentViewModel>();
                services.AddTransient<ProfessorWindow>();
                services.AddTransient<ProfessorViewModel>();
                services.AddTransient<SystemAdministratorWindow>();
                services.AddTransient<SystemAdministratorViewModel>();
            }).Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await Host!.StartAsync();

        var login = Services!.GetRequiredService<LoginWindow>();
        var loginVM = Services!.GetRequiredService<LoginViewModel>();
        login.DataContext = loginVM;
        loginVM.CurrentWindow = login;
        login.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await Host!.StopAsync();

        base.OnExit(e);
    }
}