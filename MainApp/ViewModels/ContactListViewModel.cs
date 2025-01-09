using Business.Interfaces;
using Business.Models;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace MainApp.ViewModels;

public partial class ContactListViewModel: ObservableObject
{
    private readonly IContactService _contactService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private ObservableCollection<ContactModel> _contacts = [];

    public ContactListViewModel(IContactService contactService, IServiceProvider serviceProvider)
    {
        _contactService = contactService;
        _serviceProvider = serviceProvider;
        LoadContacts();

        _contacts = new ObservableCollection<ContactModel>(_contactService.AllContacts());
    }
    private void LoadContacts()
    {
        Contacts.Clear();
        foreach (var contact in _contactService.AllContacts())
        {
            Contacts.Add(contact);
        }
    }

    [RelayCommand]
    private void DeleteContact(ContactModel contactModel)
    {
        _contactService.DeleteContact(contactModel);
        LoadContacts();
    }

        [RelayCommand]
    private void GoToAddView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactAddViewModel>();
    }
    [RelayCommand]
    private void GoToDetailsView(ContactModel contactModel)
    {
        var contactDetailsViewModel = _serviceProvider.GetRequiredService<ContactDetailsViewModel>();
        contactDetailsViewModel.ContactModel = contactModel;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = contactDetailsViewModel;
    }
}
