using Business.Dtos;
using Business.Helpers;
using Business.Models;
namespace Business.Factories;

public static class ContactFactory
{
    public static ContactRegistrationForm Create() => new();
    public static ContactModel Create(ContactRegistrationForm form) => new()
    {
        Id = IdGenerator.GenerateUniqueId(),
        FirstName = form.FirstName,
        LastName = form.LastName,
        Email = form.Email,
        PhoneNumber = form.PhoneNumber,
        Address = form.Address,
        ZipCode = form.ZipCode,
        City = form.City
    };
}
