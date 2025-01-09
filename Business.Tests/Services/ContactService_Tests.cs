using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;
using System.Text.Json;

public class ContactService_Tests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly IContactService _contactService;

    public ContactService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _contactService = new ContactService(_fileServiceMock.Object);
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

    // Detta test är från videon Tips & Trix - Skapa en fungerande applikation med tester(https://www.youtube.com/watch?v=mdzDKUO3jKQ&t=1s)
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

}