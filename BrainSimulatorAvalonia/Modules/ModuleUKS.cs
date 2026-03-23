// BrainSimulatorAvalonia/Modules/ModuleUKS.cs
// Ported from BrainSimulator/Modules/ModuleUKS.cs for Avalonia
using System;
using System.Collections.Generic;
using UKS;

namespace BrainSimulatorAvalonia.Modules
{
    public partial class ModuleUKS : ModuleBase
    {
        public string fileName = "";
        // public UKS.UKS theUKS = null; // handled by ModuleHandler
        public ModuleUKS()
        {
            allowMultipleDialogs = true;
        }

        public override void Fire()
        {
            Init();
        }

        public override void Initialize()
        {
            // Ported from WPF: Suspend engine, initialize UKS, notify modules, resume engine
            MainWindow.SuspendEngine();
            UKSInitialized();
            MainWindow.ResumeEngine();
        }

        public override void SetUpBeforeSave()
        {
            base.SetUpBeforeSave();
            if (!string.IsNullOrEmpty(fileName))
            {
                MainWindow.theUKS.SaveUKStoXMLFile();
            }
        }

        public override void SetUpAfterLoad()
        {
            // Ported: Load UKS from XML or re-init
            GetUKS();
            base.SetUpAfterLoad();
            if (!string.IsNullOrEmpty(fileName))
            {
                fileName = BrainSimulatorAvalonia.Utils.RebaseFolderToCurrentDevEnvironment(fileName);
                MainWindow.theUKS.LoadUKSfromXMLFile();
            }
            else
            {
                MainWindow.theUKS = new UKS.UKS();
            }
        }

        // Ported GetUKS and UKSInitialized from WPF
        public void GetUKS()
        {
            // In WPF, this would return the UKS list; here, ensure UKS is available
            // No-op if handled by MainWindow/module handler
        }

        public void UKSInitialized()
        {
            // Notify other modules of UKS initialization if needed
            // In WPF, this is a placeholder for module notification logic
        }
    }
}