using System;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{

    [Table(Name = "addressbook")]
    public class ContactData : IComparable<ContactData>, IEquatable<ContactData>
    {
        private string allPhones;


        public override string ToString()
        {
            return "name=" + LastName;
        }

        public int CompareTo(ContactData other)
        {
            if (ReferenceEquals(other, null))
                return 1;
            return LastName.CompareTo(other.LastName);
      
        }

        public bool Equals(ContactData other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return LastName == other.LastName;
        }
        public override int GetHashCode()
        {
            return LastName.GetHashCode();
        }

        public ContactData(string lastName)
        {
            LastName = lastName;
        }

        public ContactData(string lastName, string firstName)
        {
            LastName = lastName;
            FirstName = firstName;
        }
        public ContactData()
        {

        }

        public string Address
        {
            get; set;
        }

        public string Mobile
        {
            get; set;

        }

        public string Email
        {
            get; set;
        }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                    return allPhones;
                else
                    return (CleanUp(HomePhone) + CleanUp(Mobile) + CleanUp(WorkPhone)).Trim();
            }

            set { allPhones = value; }
        }
        [Column(Name = "firstname")]
        public string FirstName { get; set; }


        private string CleanUp(string phone)
        {
            if (phone == null || phone =="")
                return "";
            return Regex.Replace(phone, "[-()]", "") + "\r\n";
     //      return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") +"\r\n";
        }

        [Column(Name = "work")]
        public string WorkPhone
        {
            get; set;
        }
        [Column(Name = "lastname")]
        public string LastName
        {
            get; set;
        }
        [Column(Name = "title")]
        public string Title
        {
            get; set;
        }
        [Column(Name = "company")]
        public string Company
        {
            get; set;
        }
        [Column(Name = "id"),PrimaryKey, Identity]
        public string Id
        {
            get; set;
        }
        public string HomePhone { get; set; }
    }
}
