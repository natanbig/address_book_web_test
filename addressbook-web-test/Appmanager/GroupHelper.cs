using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace WebAddressbookTests
{
    public class GroupHelper:HelpBase
    {
        public GroupHelper(ApplicationManager manager):base (manager)
        {
        }

        public GroupHelper Modify(int v, GroupData newData)
        {
            manager.Navigator.GoToGroupPage();
            if(!IsGroupCreated(v))
            {
                GroupData groupdata = new GroupData("nana", "papapa", "dfhfgjdshf");
                CreateGroup(groupdata);
            }
            SelectGroup(v);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupPage();
            return this;
        }

        private List<GroupData> groupCashe = null;


        public List<GroupData> GetGroupList()
        {   if (groupCashe == null)
            {
                groupCashe = new List<GroupData>();
                List<GroupData> groups = new List<GroupData>();  //Create object for store collection
                manager.Navigator.GoToGroupPage();
                ICollection<IWebElement> elemetns = driver.FindElements(By.CssSelector("span.group")); //Count all groups on groups page and store them into ICollection
                foreach (IWebElement element in elemetns)
                {
                    groupCashe.Add(new GroupData(null) {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }

                string allGroupNames = driver.FindElement(By.CssSelector("div#content form")).Text;
                string[] parts = allGroupNames.Split('\n');
                int shift = groupCashe.Count - parts.Length;
                for (int i = 0; i<groupCashe.Count; i++)
                {
                    if (i < shift)
                        groupCashe[i].Name ="";
                    else
                    groupCashe[i].Name = parts[i-shift].Trim();

                }
                
            }
            return new List<GroupData>(groupCashe);

        }

        public int GetGroupsCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }

        private bool IsGroupCreated(int v)
        {
            return (IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + (v+1) + "]")));
        }

        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCashe = null;
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public GroupHelper RemoveGroup(int p)
        {
            manager.Navigator.GoToGroupPage();
            SelectGroup(p);
            RemoveGroup();
            ReturnToGroupPage();
            return this;
        }



        public GroupHelper CreateGroup(GroupData group)
        {
            manager.Navigator.GoToGroupPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupPage();
            return this;
        }

        public GroupHelper FillGroupForm(GroupData groupData)
        {

            Type(By.Name("group_name"), groupData.Name);
            Type(By.Name("group_header"), groupData.Header);
            Type(By.Name("group_footer"), groupData.Footer);
            return this;
        }



        public GroupHelper  RemoveGroup()
        {
            driver.FindElement(By.XPath("(//input[@name='delete'])[2]")).Click();
            groupCashe = null;
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            //Submit group creation
            driver.FindElement(By.Name("submit")).Click();
            groupCashe = null;
            return this;
        }
        public GroupHelper InitGroupCreation()
        {
            //Init new group creation
            driver.FindElement(By.Name("new")).Click();
            return this;
        }
        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
            return this;
        }
        public GroupHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }
        public GroupHelper ReturnToGroupPage()
        {
            //Return to groups page
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

    }
}
