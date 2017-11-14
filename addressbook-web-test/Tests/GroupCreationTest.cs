using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests: AuthTestBase
    {

        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for(int i=0; i<5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromCSVFile()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0]){
                    Header = parts[1],
                    Footer = parts[2]
                });
            }

            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXMLFile()
        {
            List<GroupData> groups = new List<GroupData>();
            return (List<GroupData>)
                new XmlSerializer(typeof(List<GroupData>))
                .Deserialize(new StreamReader(@"groups.xml"));
            
        }




        [Test, TestCaseSource("GroupDataFromXMLFile")]
        public void GroupCreationTest(GroupData group)
        {


            List<GroupData> oldGroups = app.Groups.GetGroupList();
            app.Groups.CreateGroup(group);

 //           Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupsCount());
            Assert.AreEqual(oldGroups.Count+1, app.Groups.GetGroupsCount());
            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            
        }





        [Test]
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData();
            group.Name = "a'a";
            group.Header = "";
            group.Footer = "";
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            app.Groups.CreateGroup(group);
            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
        [Test]
        public void TestDBConnectivity()
        {
            DateTime start = DateTime.Now;
            List<GroupData> fromUI = app.Groups.GetGroupList();
            DateTime end = DateTime.Now;
            Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            AddressBookDB db = new AddressBookDB();
            List<GroupData> fromDb = (from g in db.Groups select g).ToList();
            db.Close();
            end = DateTime.Now;
            Console.Out.WriteLine(end.Subtract(start));

        }
    }
}
