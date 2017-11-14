using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    public class ContactModificationTests: AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newContact = new ContactData();
            newContact.FirstName = "Bobo";
            newContact.LastName = "Klabo";
            newContact.Mobile = "0511111111";
            newContact.Email = "bobo@gmail.com";
            newContact.Company = "Nikayon";
            newContact.Title = "Dr";
            newContact.Address = "Kefar Kasem";
            List<ContactData> oldContancts = app.Contacts.GetContactsList();
            app.Contacts.Modify(0, newContact);
            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContancts[0].LastName = newContact.LastName;
            oldContancts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContancts, newContacts);

        }
    }
}
