using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Text.Json;

namespace Business.Services;

public class ContactService : IContactService
{
    private readonly IFileService _fileService;
    public List<ContactModel> Contacts { get; private set; } = [];

    public ContactService(IFileService fileService)
    {
        _fileService = fileService;
        LoadContacts();
    }

    public bool CreateContact(ContactRegistrationForm form)
    {
        var contact = ContactFactory.Create(form);
        Contacts.Add(contact);

        //var json = JsonSerializer.Serialize(Contacts);
        //var result = _fileService.SaveContentToFile(json);
        return SaveContacts();
    }

    // Kod generarad av ChatGPT 4o för att kunna uppdatera en kontakt
    public bool UpdateContact(ContactModel updatedContact)
    {
        var existingContact = Contacts.FirstOrDefault(c => c.Id == updatedContact.Id);
        if (existingContact != null)
        {
            existingContact.FirstName = updatedContact.FirstName;
            existingContact.LastName = updatedContact.LastName;
            existingContact.Email = updatedContact.Email;
            existingContact.PhoneNumber = updatedContact.PhoneNumber;
            existingContact.Address = updatedContact.Address;
            existingContact.ZipCode = updatedContact.ZipCode;
            existingContact.City = updatedContact.City;

            return SaveContacts();
        }
        return false;
    }

    // Kod generarad av ChatGPT 4o för att kunna ta bort en kontakt
    public bool DeleteContact(ContactModel contactModel)
    {
        var existingContact = Contacts.FirstOrDefault(c => c.Id == contactModel.Id);
        if (existingContact != null)
        {
            Contacts.Remove(existingContact);
            return SaveContacts();
        }
        return false;
    }

    public IEnumerable<ContactModel> AllContacts()
    {
        var content = _fileService.GetContentFromFile();
        try
        {
            Contacts = JsonSerializer.Deserialize<List<ContactModel>>(content)!;
        }
        catch
        {
            Contacts = [];
        }
        return Contacts;
    }

    // Generarad av ChatGTP 4o för att kontakter ska laddas in direkt vid start. 
    private void LoadContacts()
    {
        var content = _fileService.GetContentFromFile();
        try
        {
            Contacts = JsonSerializer.Deserialize<List<ContactModel>>(content) ?? new List<ContactModel>();
        }
        catch
        {
            Contacts = new List<ContactModel>();
        }
    }

    private bool SaveContacts()
    {
        var json = JsonSerializer.Serialize(Contacts);
        return _fileService.SaveContentToFile(json);
    }
}
