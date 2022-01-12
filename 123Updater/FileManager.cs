using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _123Updater
{
    static class FileManager
    {
        public static string FOLDER_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\r0p3\";
        private static string FILENAME = "name.r0p3";
        private static string FILEVERSION = "version.r0p3";

        public static string name = "";

        public static void setup()
        {
            if (!File.Exists(FOLDER_PATH + FILENAME))
                File.WriteAllText(FOLDER_PATH + FILENAME, "");
            if (hasName())
                getName();
            else
                name = "newUser";
            checkVersionFile();
        }

        private static void getName()
        {
            name = File.ReadAllText(FOLDER_PATH + FILENAME);
        }

        private static bool hasName()
        {
            return (File.Exists(FOLDER_PATH + FILENAME) && File.ReadAllText(FOLDER_PATH + FILENAME).Length > 0);
        }

        public static void saveName(string newName)
        {
            name = newName;
            File.WriteAllText(FOLDER_PATH + FILENAME, name);
        }

        public static void userInputName()
        {
            name = "";
            while (name == "")
            {
                saveName("NewUser");
                if (name == "")
                    Console.WriteLine("Name needs to be longer then 0 characters");
            }
        }

        public static void checkVersionFile()
        {
            if (File.Exists(FOLDER_PATH + FILEVERSION) && File.ReadAllText(FOLDER_PATH + FILEVERSION).Length == 0)
            {
                File.WriteAllText(FOLDER_PATH + FILEVERSION, "0");
            }
        }

        public static int getVersion()
        {
            return int.Parse(File.ReadAllText(FOLDER_PATH + FILEVERSION));
        }

        public static void saveVersion(string version)
        {
            File.WriteAllText(FOLDER_PATH + FILEVERSION, version.ToString());
        }
    }
}
