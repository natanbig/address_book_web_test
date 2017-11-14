
using System;
using LinqToDB.Mapping;
namespace WebAddressbookTests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>,IComparable<GroupData>
    {
        
        public bool Equals(GroupData other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString() {
            return "name=" + Name;
        }

        public int CompareTo(GroupData other)
        {
            if (ReferenceEquals(other, null))
                return 1;
            return Name.CompareTo(other.Name);
        }

        public GroupData()
        {

        }
        public GroupData(string name)
        {
            this.name = name;
        }
		private string name;
        private string header;
		private string footer;
        private string id;


        [Column(Name= "group_name"), NotNull]
        public string Name { get { return name; }  set { name = value; } }
        [Column(Name = "group_header"), NotNull]
        public string Header { get { return header; } set { header = value; } }
        [Column(Name = "group_footer"), NotNull]
        public string Footer { get { return footer; } set { footer = value; } }
        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public GroupData (string name, string header, string footer)
        {
            this.name = name;
            this.header = header;
            this.footer = footer;
        }




    }
}
