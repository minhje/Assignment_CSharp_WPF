using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using MainApp.ViewModels;
using MainApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace MainApp;

public partial class App : Application
{
    private IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<IFileService>(provider => new FileService("contacts.json"));
                services.AddSingleton<ContactRegistrationForm>();
                services.AddTransient<IContactService, ContactService>();
                services.AddTransient<ContactModel>();


                services.AddTransient<ContactListViewModel>();
                services.AddTransient<ContactListView>();

                services.AddTransient<ContactAddViewModel>();
                services.AddTransient<ContactAddView>();

                services.AddTransient<ContactEditViewModel>();
                services.AddTransient<ContactEditView>();

                services.AddTransient<ContactDetailsViewModel>();
                services.AddTransient<ContactDetailsView>();


                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<ContactListViewModel>();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
