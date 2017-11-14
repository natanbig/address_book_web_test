using OpenQA.Selenium;

namespace WebAddressbookTests
{
    public class NavigationHelper:HelpBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL):base (manager)
        {
            this.baseURL = baseURL;
        }
        public void GoToGroupPage()
        {
            if (driver.Url == baseURL + "/addressbook/group.php"
                && IsElementPresent(By.Name("new")))
            { }
            {
                driver.FindElement(By.LinkText("groups")).Click();
            }
        }
        public void GoToHomePage()
        {
            if (driver.Url == baseURL + "/addressbook/")
                return;
            driver.Navigate().GoToUrl(baseURL + "/addressbook/");
        }




    }
}
