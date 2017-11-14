using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using System.Threading;

namespace WebAddressbookTests
{
   public class ApplicationManager
    {
        protected LoginHelper loginHelper;
        protected NavigationHelper navigator;
        private GroupHelper grouphelper;
        private ContactHelper contactHelper;
        protected string baseURL;
        protected IWebDriver driver;
        private StringBuilder verificationErrors;
        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        private ApplicationManager()
        {
            FirefoxOptions ffOptions = new FirefoxOptions();
            ffOptions.UseLegacyImplementation = true;
            ffOptions.BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            driver = new FirefoxDriver(ffOptions);
            baseURL = "http://localhost";
            verificationErrors = new StringBuilder();
            loginHelper = new LoginHelper(this);
            navigator = new NavigationHelper(this, baseURL);
            grouphelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
        }

         ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        public static ApplicationManager GetInstance()
        {
            if(!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Navigator.GoToHomePage();
                app.Value = newInstance;
        
            }
            return app.Value;
        }


        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }
        public NavigationHelper Navigator
        {
            get
            {
                return navigator;
            }
        }

        public GroupHelper Groups
        {
            get
            {
                return grouphelper;
            }
        }

        public ContactHelper Contacts
        {
            get
            {
                return contactHelper;
            }
        }
    }
}
