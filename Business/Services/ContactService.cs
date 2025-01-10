using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Text.Json;

namespace Business.Services;

public class ContactService : IContactService
{
    private readonly IFileService _fileService;
    private object _contactService;

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

        return SaveContacts();
    }

    // Kod generarad av ChatGPT 4o för att kunna uppdatera en kontakt. Tar en befintlig kontakt och uppdaterar den med nya värden.
    public bool UpdateContact(ContactModel updatedContact)
    {
        var existingContact = Contacts.FirstOrDefault(c => c.Id == updatedContact.Id); // Hittar kontakten som ska uppdateras
        if (existingContact != null) 
        {
            existingContact.FirstName = updatedContact.FirstName;
            existingContact.LastName = updatedContact.LastName;
            existingContact.Email = updatedContact.Email;
            existingContact.PhoneNumber = updatedContact.PhoneNumber;
            existingContact.Address = updatedContact.Address;
            existingContact.ZipCode = updatedContact.ZipCode;
            existingContact.City = updatedContact.City;

            return SaveContacts(); // Sparar listan till filen med den uppdaterade kontakten
        }
        return false;
    }

    // Kod generarad av ChatGPT 4o för att kunna ta bort en kontakt. Tar en befintlig kontakt och tar bort den från listan.
    public bool DeleteContact(ContactModel contactModel)
    {
        var existingContact = Contacts.FirstOrDefault(c => c.Id == contactModel.Id); // Hittar kontakten som ska tas bort
        if (existingContact != null)
        {
            Contacts.Remove(existingContact); // Tar bort kontakten från listan
            return SaveContacts(); // Sparar listan till filen
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
    public void LoadContacts()
    {
        var content = _fileService.GetContentFromFile(); // Hämtar innehållet från filen
        try
        {
            Contacts = JsonSerializer.Deserialize<List<ContactModel>>(content) ?? []; // Laddar in kontakter från filen
        }
        catch
        {
            Contacts = []; // Om det inte går att ladda in kontakter retuneras en tom lista
        }
    }

    public bool SaveContacts()
    {
        var json = JsonSerializer.Serialize(Contacts);
        return _fileService.SaveContentToFile(json);
    }
}