using Business.Dtos;
using Business.Factories;
using Business.Models;

namespace Business.Tests.Factories;

public class ContactFactory_Tests
{
    [Fact]

    public void Create_ShouldReturnContactRegistrationForm()
    {
        // Act
        var result = ContactFactory.Create();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ContactRegistrationForm>(result);
    }


    // Detta test är från videon Tips & Trix - Skapa en fungerande applikation med tester(https://www.youtube.com/watch?v=mdzDKUO3jKQ&t=1s)
    [Fact]
    public void Create_ShouldReturnContact_WhenContactRegistrationFormIsProvided()
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
        // Act
        var result = ContactFactory.Create(contactRegistrationForm);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ContactModel>(result);
        Assert.Equal(contactRegistrationForm.FirstName, result.FirstName);
        Assert.Equal(contactRegistrationForm.LastName, result.LastName);
        Assert.Equal(contactRegistrationForm.Email, result.Email);
        Assert.Equal(contactRegistrationForm.PhoneNumber, result.PhoneNumber);
        Assert.Equal(contactRegistrationForm.Address, result.Address);
        Assert.Equal(contactRegistrationForm.ZipCode, result.ZipCode);
        Assert.Equal(contactRegistrationForm.City, result.City);
    }
}
