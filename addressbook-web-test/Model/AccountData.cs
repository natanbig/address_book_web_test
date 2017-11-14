
namespace WebAddressbookTests
{
    public class AccountData
    {
        private string username;
        private string password;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public  AccountData(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
