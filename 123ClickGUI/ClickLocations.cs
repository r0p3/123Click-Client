using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _123ClickGUI
{
    public class ClickLocations
    {
        public delegate void update();
        public event update onRecordsChanged;

        private static string FILEPATH;
        public static string LOCATIONPATH;
        public ClickLocations()
        {
            FILEPATH = FileManager.FOLDER_PATH + "ClickLocations.xml";
            LOCATIONPATH = FileManager.FOLDER_PATH + "location.r0p3";
            createFile();
        }

        private void createFile()
        {
            if (!File.Exists(FILEPATH) || File.ReadAllBytes(FILEPATH).Length == 0)
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlElement root = xmlDocument.CreateElement("ClickLocations");
                xmlDocument.AppendChild(root);
                saveXml(xmlDocument);
            }
        }

        public void addRecordWithName(string name, int x, int y)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(FILEPATH);
            XmlNode root = xmlDocument.SelectSingleNode("ClickLocations");
            XmlElement clickLocation = xmlDocument.CreateElement("ClickLocation");
            root.AppendChild(clickLocation);
            XmlAttribute id = xmlDocument.CreateAttribute("name");
            id.Value = name;
            clickLocation.Attributes.Append(id);

            XmlElement locationX = xmlDocument.CreateElement("X");
            locationX.InnerText = x.ToString();
            clickLocation.AppendChild(locationX);
            XmlElement locationY = xmlDocument.CreateElement("Y");
            locationY.InnerText = y.ToString();
            clickLocation.AppendChild(locationY);
            //Program.currentLocation = name;
            saveXml(xmlDocument);
            onRecordsChanged?.Invoke();
        }

        public void editRecord(string name, int x, int y)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(FILEPATH);
            XmlNode clickLocation = xmlDocument.SelectSingleNode("ClickLocations/ClickLocation[@name='" + name + "']");
            if (clickLocation != null)
            {
                clickLocation.ChildNodes[0].InnerText = x.ToString();
                clickLocation.ChildNodes[1].InnerText = y.ToString();
            }
            saveXml(xmlDocument);
            onRecordsChanged?.Invoke();
        }

        public void addRecord(int x, int y)
        {
            Console.Clear();
            Console.WriteLine("Leave field empty to cancel");
            Console.WriteLine();
            Console.WriteLine("Current location X:" + x + ", Y:" + y);
            Console.Write("Name: ");
            string name = Console.ReadLine();
            if (name != "")
                addRecordWithName(name, x, y);
        }

        public void removeRecord(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(FILEPATH);
            XmlNode clickLocation = xmlDocument.SelectSingleNode("ClickLocations/ClickLocation[@name='" + name + "']");
            if (clickLocation != null)
                clickLocation.ParentNode.RemoveChild(clickLocation);
            saveXml(xmlDocument);
            onRecordsChanged?.Invoke();
        }

        public List<string> getAllClickLocations()
        {
            List<string> locations = new List<string>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(FILEPATH);
            XmlNodeList nodeList = xmlDocument.SelectNodes("ClickLocations/ClickLocation");
            foreach (XmlNode item in nodeList)
            {
                locations.Add(item.Attributes["name"].Value);
            }
            return locations;
        }

        private void saveXml(XmlDocument xmlDocument)
        {
            xmlDocument.Save(FILEPATH);
        }

        public int getLocationX(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(FILEPATH);
            XmlNode clickLocation = xmlDocument.SelectSingleNode("ClickLocations/ClickLocation[@name='" + name + "']");
            return int.Parse(clickLocation["X"].InnerText);

        }
        public int getLocationY(string name)
        {
            List<int> location = new List<int>() { 0, 0 };
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(FILEPATH);
            XmlNode clickLocation = xmlDocument.SelectSingleNode("ClickLocations/ClickLocation[@name='" + name + "']");
            return int.Parse(clickLocation["Y"].InnerText);
        }

        public void saveLastLocation(string name)
        {
            File.WriteAllText(LOCATIONPATH, name);
        }

        public string getLastLocation()
        {
            if (File.Exists(LOCATIONPATH))
                return File.ReadAllText(LOCATIONPATH);
            else
                return "";
        }
    }
}
