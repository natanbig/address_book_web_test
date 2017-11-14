using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests:TestBase
    {
        [Test]
        public void LoginWithValidCredantials()
        {
            app.Auth.Logout();
            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);
            Assert.IsTrue(app.Auth.IsLoggedIn());
        }
        [Test]
        public void LoginWithInvalidCredantials()
        {
            app.Auth.Logout();
            AccountData account1 = new AccountData("admin", "ssss");
            app.Auth.Login(account1);
            Assert.IsFalse(app.Auth.IsLoggedIn());
        }
    }
}
