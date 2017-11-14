using NUnit.Framework;
using System;
using System.Text;

namespace WebAddressbookTests
{
   public class TestBase
    {

        protected ApplicationManager app;
        [SetUp]
        public void SetupApplicationManagerTest()
        {
            app = ApplicationManager.GetInstance();

        }

        public static Random rnd = new Random();

        public static string GenerateRandomString(int max)
        {
            
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i<l; i++)
            {
                builder.Append(Convert.ToChar(65+Convert.ToInt32(rnd.NextDouble() * 57)));
            }
            return builder.ToString();
        }
    }
}
