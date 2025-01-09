using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface IContactService
    {
        bool CreateContact(ContactRegistrationForm form);
        IEnumerable<ContactModel> AllContacts();
        bool UpdateContact(ContactModel contactModel);
        bool DeleteContact(ContactModel contactModel);
    }
}