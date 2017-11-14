using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace WebAddressbookTests


{
    class ContactCreationTests: AuthTestBase
    {
        [Test]

        public void ContactCreationTest()
        {

            
            ContactData account = new ContactData();
            account.FirstName = "Natan";
            account.LastName = "Radostin";
            account.Mobile = "0504566727";
            account.WorkPhone = "092232325";
            account.HomePhone = "0911133333";
            account.Email = "natanbig@gmail.com";
            account.Company = "Forcepoint";
            account.Title = "Mr";
            account.Address = "Kefar Sava";
            List<ContactData> oldContacts = app.Contacts.GetContactsList();
            app.Contacts.CreateContact(account);
            Assert.AreEqual(oldContacts.Count+1, app.Contacts.GetContactsListCount());
            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(account);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);


        }


        [Test]
        public void EmtyContactCreation()
        {
            ContactData account = new ContactData();
            account.FirstName = "";
            account.LastName = "";
            account.Mobile = "";
            account.Email = "";
            account.Company = "";
            account.Title = "";
            account.Address = "";
            List<ContactData> oldContacts = app.Contacts.GetContactsList();
            app.Contacts.CreateContact(account);
            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsListCount());
            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(account);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }


        [Test]
        public void TestDBConnectivityOnContactList()
        {
            DateTime start = DateTime.Now;
            List<ContactData> fromUI=app.Contacts.GetContactsList();
            DateTime end = DateTime.Now;
            Console.Out.Write(end.Subtract(start));
            start = DateTime.Now;
            AddressBookDB db = new AddressBookDB();
            List<ContactData> fromDb = (from c in db.Contacts select c).ToList();
            end = DateTime.Now;
            db.Close();
            Console.Out.Write(end.Subtract(start));
        }
    }
}
