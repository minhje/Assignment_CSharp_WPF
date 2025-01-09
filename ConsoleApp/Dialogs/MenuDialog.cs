using Business.Factories;
using Business.Interfaces;

namespace MainApp.Dialogs;

public class MenuDialog(IContactService contactService)
{
    private readonly IContactService _contactService = contactService;


    public void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("-------- MAIN MENU --------");
            Console.WriteLine("1. Add Contact");
            Console.WriteLine("2. View Contacts");
            Console.WriteLine("3. Exit");
            Console.Write("Select option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddContact();
                    break;
                case "2":
                    ViewContacts();
                    break;
                case "3":
                    ExitApplication();
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public void AddContact()
    {
        var form = ContactFactory.Create();

        Console.Clear();
        Console.WriteLine("-------- ADD CONTACT --------");
        Console.Write("First Name: ");
        form.FirstName = Console.ReadLine()!;

        Console.Write("Last Name: ");
        form.LastName = Console.ReadLine()!;

        Console.Write("Email: ");
        form.Email = Console.ReadLine()!;

        Console.Write("Phone number: ");
        form.PhoneNumber = Console.ReadLine()!;

        Console.Write("Address: ");
        form.Address = Console.ReadLine()!;

        Console.Write("ZIP code: ");
        form.ZipCode = Console.ReadLine()!;

        Console.Write("City: ");
        form.City = Console.ReadLine()!;

        var result = _contactService.CreateContact(form);

        if (result)
        {
            Console.WriteLine("");
            Console.WriteLine("Contact added successfully. Press any key to continue...");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("");
            Console.WriteLine("Failed to add contact. Press any key to continue...");
            Console.ReadKey();
        }
    }

    public void ViewContacts()
    {
        Console.Clear();
        Console.WriteLine("-------- VIEW CONTACTS --------");
        var contacts = _contactService.AllContacts();

        foreach (var contact in contacts)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($" Id: {contact.Id}");
            Console.WriteLine($" Name: {contact.FirstName} {contact.LastName}");
            Console.WriteLine($" Email: {contact.Email}");
            Console.WriteLine($" Phone number: {contact.PhoneNumber}");
            Console.WriteLine($" Address: {contact.Address} - {contact.ZipCode} - {contact.City}");
            Console.WriteLine("-------------------------------------------");
        }

        if (contacts.Count() == 0)
        {
            Console.WriteLine("No contacts found.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public void ExitApplication()
    {
        Console.Clear();
        Console.WriteLine("Are you sure you want to exit the application (y/n)? ");
        var response = Console.ReadLine()!;

        if (response.ToLower() == "y")
        {
            Console.WriteLine("Exiting application...");
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
