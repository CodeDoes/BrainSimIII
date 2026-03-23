using System;
// using Python.Runtime;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UKS;

namespace BrainSimulatorAvalonia
{
    public class ModuleHandler
    {
        public List<string> pythonModules = new();
        public List<(string, dynamic)> activePythonModules = new();
        public UKS.UKS theUKS = new();

        string pythonPath = "";
        public string PythonPath { get => pythonPath; set => pythonPath = value; }

        public string ActivateModule(string moduleType)
        {
            Thing t = theUKS.GetOrAddThing(moduleType, "AvailableModule");
            t = theUKS.CreateInstanceOf(theUKS.Labeled(moduleType));
            t.AddParent(theUKS.Labeled("ActiveModule"));
            // Integrate with MainWindow and module instantiation if needed
            // For .py modules, add to pythonModules
            if (!moduleType.Contains(".py"))
            {
                // TODO: Integrate with MainWindow for C# modules
            }
            else
            {
                pythonModules.Add(t.Label);
            }
            return t.Label;
        }

        public void DeactivateModule(string moduleLabel)
        {
            Thing t = theUKS.Labeled(moduleLabel);
            if (t == null) return;
            for (int i = 0; i < t.Relationships.Count; i++)
            {
                Relationship r = t.Relationships[i];
                theUKS.DeleteThing(r.target);
            }
            theUKS.DeleteThing(t);
        }

        public List<string> GetListOfExistingPythonModuleTypes()
        {
            List<string> pythonFiles = new();
            if (pythonPath == "no") return pythonFiles;
            try
            {
                var filesInDir = Directory.GetFiles(".", "m*.py").ToList();
                foreach (var file in filesInDir)
                {
                    if (file.StartsWith("utils")) continue;
                    if (file.Contains("template")) continue;
                    pythonFiles.Add(Path.GetFileName(file));
                }
            }
            catch { }
            return pythonFiles;
        }

        // --- Python engine integration (Avalonia, cross-platform) ---
        // Python integration is temporarily disabled for build compatibility.
        public bool InitPythonEngine() { return false; }
        public bool ClosePythonEngine() { return true; }
        public void Close(string moduleLabel) { }
        public void RunScript(string moduleLabel) { }

        public void CreateEmptyUKS()
        {
            theUKS = new UKS.UKS();
            if (theUKS.Labeled("BrainSim") == null)
                theUKS.AddThing("BrainSim", null);
            theUKS.GetOrAddThing("AvailableModule", "BrainSim");
            theUKS.GetOrAddThing("ActiveModule", "BrainSim");
            InsertMandatoryModules();
        }

        public void InsertMandatoryModules()
        {
            Console.WriteLine("InsertMandatoryModules entered");
            ActivateModule("UKS");
            ActivateModule("UKSStatement");
        }
    }
}
