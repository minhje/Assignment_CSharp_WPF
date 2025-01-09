using Business.Interfaces;
using Business.Services;
using MainApp.Dialogs;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IFileService>(provider => new FileService("contacts.json"))

    .AddSingleton<IContactService, ContactService>()

    .AddSingleton<MenuDialog>()
    .BuildServiceProvider();

var menuDialog = serviceProvider.GetRequiredService<MenuDialog>();
menuDialog.MainMenu();