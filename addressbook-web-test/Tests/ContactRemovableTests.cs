using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    public class ContactRemovableTests : AuthTestBase
    {
        [Test]
        public void  ContactRemovableTest()
        {
            List<ContactData> oldContacts = app.Contacts.GetContactsList();
            app.Contacts.RemoveContact(0);
            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
