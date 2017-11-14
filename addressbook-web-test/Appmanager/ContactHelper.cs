using NUnit.Framework;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;

namespace WebAddressbookTests
{
   public class ContactHelper:HelpBase
    {
        private bool acceptNextAlert = true;

        public ContactHelper(ApplicationManager manager):base(manager)
        {
         
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;
            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workphone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            return new ContactData(firstName, lastName)
            {
                Address = address,
                Mobile = mobilePhone,
                Email = email,
                HomePhone = homePhone,
                WorkPhone = workphone
            };
        }

        public ContactHelper Modify(int p, ContactData newContact)
        {
            if (!IsContactExist(p))
            {
                ContactData contactdata = new ContactData();
                contactdata.FirstName = "nnn";
                contactdata.LastName = "owjdsajd";
                contactdata.Address = "sdasd";
                contactdata.Company = "sdgfdgdfg";
                contactdata.Email = "sdgerrewqewqe";
                contactdata.Mobile = "";
                contactdata.Title = "Mr";
                CreateContact(contactdata);

            }
            InitContactModification(p);
            FillContactForm(newContact);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public int GetContactsListCount()
        {
            return driver.FindElements(By.CssSelector("td:nth-child(2)")).Count;
            
        }

        private List<ContactData> contactsCash = null;

        public List<ContactData> GetContactsList()
        {
            if (contactsCash == null)
            {
                contactsCash = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("td:nth-child(2)"));
                foreach (IWebElement element in elements)
                {
                    contactsCash.Add(new ContactData(element.Text));
                }

            }


            return new List<ContactData>(contactsCash);
        }


        private bool IsContactExist(int p)
        {
            return (IsElementPresent(By.XPath("(//img[@alt='Edit'])[" + (p+1) + "]")));

        }

        private ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactsCash = null;
            return this;
        }

        private ContactHelper InitContactModification(int p)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (p+1) + "]")).Click();
            return this;
        }

        public ContactHelper RemoveContact(int p)
        {
            SelectContactFromList(p);
            DeleteContact();
            ReturnToHomePage();
            contactsCash = null;
            return this;
        }

        public ContactHelper CreateContact(ContactData account)
        {
            InitContactCreation();
            FillContactForm(account);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }



        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData account)
        {
            Type(By.Name("firstname"),(account.FirstName));
            Type(By.Name("lastname"),(account.LastName));
            Type(By.Name("title"), (account.Title));
            Type(By.Name("company"), (account.Company));
            Type(By.Name("address"), (account.Address));
            Type(By.Name("mobile"), (account.Mobile));
            Type(By.Name("work"), (account.WorkPhone));
            Type(By.Name("home"), (account.HomePhone));
            Type(By.Name("email"), (account.Email));
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactsCash = null;
            return this;
        }

        public ContactHelper SelectContactFromList(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
            return this;
        }

        public ContactHelper DeleteContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Delete 1 addresses[\\s\\S]$"));
            return this;
        }
        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

        public int GetNumberOfResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }



    }
}
