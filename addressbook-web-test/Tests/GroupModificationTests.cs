using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData();
            newData.Name = "newnewnew";
            newData.Header = null;
            newData.Footer = null;

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            

            app.Groups.Modify(0, newData);
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupsCount());
            List<GroupData> newGroup = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            
            oldGroups.Sort();
            newGroup.Sort();
            Assert.AreEqual(oldGroups, newGroup);
        }
    }
}
