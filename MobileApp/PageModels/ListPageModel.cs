using Business.Dtos;
using Business.Models;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MobileApp.PageModels;

public partial class ListPageModel : ObservableObject
{
    private readonly ContactService _contactService;

    public ListPageModel(ContactService contactService)
    {
        _contactService = contactService;
        UpdateContactList();
    }

    [ObservableProperty]
    private ContactRegistrationForm _registrationForm = new();

    [ObservableProperty]
    private ObservableCollection<ContactModel> _contactList = [];

    [RelayCommand]
    public void AddContactToList()
    {
        if (RegistrationForm != null)
        {
            var result = _contactService.CreateContact(RegistrationForm);
            if (result)
            {
                ContactList = (ObservableCollection<ContactModel>)_contactService.AllContacts();

            }
        }
    }
    public void UpdateContactList()
    {
        ContactList = new ObservableCollection<ContactModel>(_contactService.Contacts.Select(contact => new ContactModel()).ToList());
    }
}
