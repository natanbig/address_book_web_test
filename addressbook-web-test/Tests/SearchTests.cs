using System;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class SearchTests : AuthTestBase
    {
        [Test]
        public void TestSearch()
        {
            System.Console.Out.Write(app.Contacts.GetNumberOfResults());
        }

    }
}
