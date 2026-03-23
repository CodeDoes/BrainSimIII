using System;
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
            // TODO: Integrate with MainWindow and module instantiation
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

        // TODO: Add Python engine integration and other methods as needed
    }
}
