using Business.Dtos;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;
using System.Text.Json;

public class ContactService_Tests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly IContactService _contactService;
    private readonly List<ContactModel> _contacts;

    public ContactService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _contactService = new ContactService(_fileServiceMock.Object);
        _contacts = new List<ContactModel>();

    }

    [Fact]
    public void ContactModel_DisplayName_ShouldReturnFullName()
    {
        // Arrange
        var contact = new ContactModel
        {
            FirstName = "John",
            LastName = "Doe"
        };

        
        // Act
        var result = contact.DisplayName;
        
        // Assert
        Assert.Equal("John Doe", result);
    }

    [Fact]
    public void Save_ShouldReturnTrue_AddUserToList_AndSaveToFile()
    {
        // Arrange
        var contactRegistrationForm = new ContactRegistrationForm()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@domain.com",
            PhoneNumber = "1234567890",
            Address = "Teststreet 1",
            ZipCode = "12345",
            City = "Testcity"
        };

        _fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>())).Returns(true);

        // Act

        var result = _contactService.CreateContact(contactRegistrationForm);

        // Assert
        Assert.True(result);
        _fileServiceMock.Verify(fs => fs.SaveContentToFile(It.IsAny<string>()), Times.Once);
    }

    /* Detta test är från videon Tips & Trix - Skapa en fungerande applikation med tester(https://www.youtube.com/watch?v=mdzDKUO3jKQ&t=1s) 
     * En lista med kontakter skapas i Arrange för att sedan retunera listan när AllContacts anropas.
     * Kontrollerar om det finns en kontakt i listan och att listan inte är tom samt att kontakten har rätt värden. 
     */
    [Fact]
    public void GetAll_ShouldReturnListOfContacts()
    {
        // Arrange
        List<ContactModel> expected = [new ContactModel 
        {
            Id = "1",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@domain.com",
            PhoneNumber = "123456789",
            Address = "Teststreet 1",
            ZipCode = "12345",
            City = "Testcity"
        }];
        var json = JsonSerializer.Serialize(expected);

        _fileServiceMock.Setup(fs => fs.GetContentFromFile()).Returns(json);

        // Act
        var result = _contactService.AllContacts();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result); 

        Assert.Equal(expected[0].Id, result.First().Id); 
        Assert.Equal(expected[0].FirstName, result.First().FirstName);
        Assert.Equal(expected[0].LastName, result.First().LastName);
        Assert.Equal(expected[0].Email, result.First().Email);
        Assert.Equal(expected[0].PhoneNumber, result.First().PhoneNumber);
        Assert.Equal(expected[0].Address, result.First().Address);
        Assert.Equal(expected[0].ZipCode, result.First().ZipCode);
        Assert.Equal(expected[0].City, result.First().City);
    }

    /* Test generat av ChatGPT 4o för att testa om en kontakt kan uppdateras med UpdateContact metoden. Skapar en mock av FileService 
     * och skapar en instans av ContactService. Skapar en befintlig kontakt och en uppdaterad kontakt. Lägger till den befintliga kontakten och retunerar true från SaveContentToFile metoden. 
     * Kontrollerar sedan att uppdateringen har skett och att SaveContentToFile metoden har anropats en gång. */
    [Fact]
    public void UpdateContact_ShouldUpdateExistingContact()
    {
        // Arrange
        var mockFileService = new Mock<IFileService>();
        var contactService = new ContactService(mockFileService.Object);

        var existingContact = new ContactModel
        {
            Id = "1",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
            Address = "123 Main St",
            ZipCode = "12345",
            City = "Anytown"
        };

        var updatedContact = new ContactModel
        {
            Id = "1",
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@example.com",
            PhoneNumber = "0987654321",
            Address = "456 Elm St",
            ZipCode = "54321",
            City = "Othertown"
        };

        contactService.Contacts.Add(existingContact);

        mockFileService.Setup(fs => fs.SaveContentToFile(It.IsAny<string>())).Returns(true);

        // Act
        var result = contactService.UpdateContact(updatedContact);

        // Assert
        Assert.True(result);
        Assert.Equal("Jane", existingContact.FirstName);
        Assert.Equal("Doe", existingContact.LastName);
        Assert.Equal("jane.doe@example.com", existingContact.Email);
        Assert.Equal("0987654321", existingContact.PhoneNumber);
        Assert.Equal("456 Elm St", existingContact.Address);
        Assert.Equal("54321", existingContact.ZipCode);
        Assert.Equal("Othertown", existingContact.City);
        mockFileService.Verify(fs => fs.SaveContentToFile(It.IsAny<string>()), Times.Once);
    }

    /* Test genererat av ChatGTP 4o för att säkerhetställa att kontakter laddas in korrekt. 
     * En ny kontakt skapas i Arrange för att sedan ladda in kontakten och testa om expectedContacts stämmer överrens med kontakten i filen. */
    [Fact]
    public void LoadContacts_ShouldLoadContactsFromFile()
    {
        // Arrange
        var expectedContacts = new List<ContactModel> 
    {
        new() {
            Id = "1",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
            Address = "123 Main St",
            ZipCode = "12345",
            City = "Anytown"
        }
    };
        var json = JsonSerializer.Serialize(expectedContacts);
        _fileServiceMock.Setup(fs => fs.GetContentFromFile()).Returns(json);

        // Act
        _contactService.LoadContacts(); 

        // Assert
        Assert.Equal(expectedContacts.Count, _contactService.Contacts.Count);
        Assert.Equal(expectedContacts[0].Id, _contactService.Contacts[0].Id);
        Assert.Equal(expectedContacts[0].FirstName, _contactService.Contacts[0].FirstName);
        Assert.Equal(expectedContacts[0].LastName, _contactService.Contacts[0].LastName);
        Assert.Equal(expectedContacts[0].Email, _contactService.Contacts[0].Email);
        Assert.Equal(expectedContacts[0].PhoneNumber, _contactService.Contacts[0].PhoneNumber);
        Assert.Equal(expectedContacts[0].Address, _contactService.Contacts[0].Address);
        Assert.Equal(expectedContacts[0].ZipCode, _contactService.Contacts[0].ZipCode);
        Assert.Equal(expectedContacts[0].City, _contactService.Contacts[0].City);
    }

    /* Test genererat av ChatGTP 4o för att testa att kontakter sparas korrekt. 
     * En lista med kontakter skapas i Arrange för att sedan sparas till fil och retunera true. 
     * Kontrollerar sedan att SaveContentToFile metoden har anropats en gång. */
    [Fact]
    public void SaveContacts_ShouldSaveContactsToFile()
    {
        // Arrange
        var contactsToSave = new List<ContactModel>
    {
        new ContactModel
        {
            Id = "1",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
            Address = "123 Main St",
            ZipCode = "12345",
            City = "Anytown"
        }
    };

        _contactService.Contacts.Clear();
        _contactService.Contacts.AddRange(contactsToSave);

        var json = JsonSerializer.Serialize(contactsToSave);
        _fileServiceMock.Setup(fs => fs.SaveContentToFile(json)).Returns(true);

        // Act
        var result = _contactService.SaveContacts();

        // Assert
        Assert.True(result);
        _fileServiceMock.Verify(fs => fs.SaveContentToFile(It.IsAny<string>()), Times.Once);
    }

    /* Test genererat av ChatGTP 4o för att testa att en kontakt tas bort korrekt. 
     * En kontakt skapas i Arrange för att sedan tas bort och retunera true från SaveContentToFile metoden. 
     * Kontrollerar sedan att kontakten inte finns i listan och att SaveContentToFile metoden har anropats en gång. */
    [Fact]
    public void DeleteContact_ShouldRemoveContactFromList_AndSaveToFile()
    {
        // Arrange
        var contactToDelete = new ContactModel
        {
            Id = "1",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
            Address = "123 Main St",
            ZipCode = "12345",
            City = "Anytown"
        };

        _contactService.Contacts.Add(contactToDelete);

        _fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>())).Returns(true);

        // Act
        var result = _contactService.DeleteContact(contactToDelete);

        // Assert
        Assert.True(result);
        Assert.DoesNotContain(contactToDelete, _contactService.Contacts);
        _fileServiceMock.Verify(fs => fs.SaveContentToFile(It.IsAny<string>()), Times.Once);
    }

}


