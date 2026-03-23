using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
// TODO: Add Avalonia UI equivalents for MessageBox and settings if needed

namespace BrainSimulatorAvalonia
{
    public class XmlFile
    {
        //this is the set of moduletypes that the xml serializer will save
        static public Type[] GetModuleTypes()
        {
            Type[] listOfBs = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                               from assemblyType in domainAssembly.GetTypes()
                               where assemblyType.IsSubclassOf(typeof(ModuleBase))
                               select assemblyType).ToArray();
            List<Type> list = new List<Type>();
            for (int i = 0; i < listOfBs.Length; i++)
                list.Add(listOfBs[i]);
            // Add classes so XML saving works
            list.Add(typeof(Angle));
            list.Add(typeof(Point3DPlus));
            list.Add(typeof(PointPlus));
            return list.ToArray();
        }

        // TODO: Implement MRU list removal using Avalonia or .NET Core settings
        public static void RemoveFileFromMRUList(string filePath)
        {
            // Not implemented: Properties.Settings.Default["MRUList"]
        }

        public static bool Load(string fileName)
        {
            Stream file;
            try
            {
                file = File.Open(fileName, FileMode.Open, FileAccess.Read);
            }
            catch (Exception e)
            {
                // TODO: Replace with Avalonia dialog
                // MessageBox.Show($"Could not open file because: {e.Message}");
                RemoveFileFromMRUList(fileName);
                return false;
            }

            // first check if the required start tag is present in the file...
            byte[] buffer = new byte[200];
            file.Read(buffer, 0, 200);
            string line = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            if (line.Contains("BrainSim3Data"))
            {
                file.Seek(0, SeekOrigin.Begin);
            }
            else
            {
                file.Close();
                // TODO: Replace with Avalonia dialog
                // MessageBox.Show("File is not a valid Brain Simulator III XML file.");
                return false;
            }

            XmlSerializer reader1 = new XmlSerializer(typeof(BrainSim3Data), GetModuleTypes());
            try
            {
                MainWindow.BrainSim3Data = (BrainSim3Data)reader1.Deserialize(file);
            }
            catch (Exception e)
            {
                file.Close();
                // TODO: Replace with Avalonia dialog
                // MessageBox.Show($"Network file load failed, a blank network will be opened.\n\n{e.InnerException}", "File Load Error");
                return false;
            }

            return true;
        }

        public static bool CanWriteTo(string fileName)
        {
            return CanWriteTo(fileName, out _);
        }

        public static bool CanWriteTo(string fileName, out string message)
        {
            FileStream file1;
            message = "";
            if (File.Exists(fileName))
            {
                try
                {
                    file1 = File.Open(fileName, FileMode.Open);
                    file1.Close();
                    return true;
                }
                catch (Exception e)
                {
                    message = e.Message;
                    return false;
                }
            }
            return true;
        }

        public static bool Save(string fileName)
        {
            Stream file;
            string tempFile = "";

            if (!CanWriteTo(fileName, out string message))
            {
                // TODO: Replace with Avalonia dialog
                // MessageBox.Show($"Could not save file because: {message}");
                return false;
            }

            file = File.Create(fileName);
            Type[] extraTypes = GetModuleTypes();
            try
            {
                XmlSerializer writer = new XmlSerializer(typeof(BrainSim3Data), extraTypes);
                writer.Serialize(file, MainWindow.BrainSim3Data);
            }
            catch (Exception e)
            {
                // TODO: Replace with Avalonia dialog
                // MessageBox.Show($"Xml file write failed because: {e.InnerException?.Message ?? e.Message}");
                return false;
            }
            file.Close();
            return true;
        }
    }
}
