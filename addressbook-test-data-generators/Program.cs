using System;
using System.Collections.Generic;
using System.IO;
using WebAddressbookTests;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            StreamWriter writer = new StreamWriter(args[1]);
            string format = args[2];

            List<GroupData> groups = new List<GroupData>();
            for (int i=0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(100),
                    Footer = TestBase.GenerateRandomString(100)

                });

            }

            if (format == "csv")
            {
                writeToCsvFile(groups, writer);
            }
            else if (format == "xml")
            {
                writeToXmlFile(groups, writer);
            }
            else if (format == "json")
            {
                writeToJsonFile(groups, writer);
            }
            else
            {
                Console.Out.Write("Unrecognized format" + format);
            }
            writer.Close();
        }

        static void writeToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
            
        }

        static void writeToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void writeToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

    }
}
