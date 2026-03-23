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
            // TODO: Integrate with Avalonia MainWindow/engine suspend/resume if needed
            // UKSInitialized();
        }

        public override void SetUpBeforeSave()
        {
            base.SetUpBeforeSave();
            if (!string.IsNullOrEmpty(fileName))
            {
                // TODO: Save UKS to XML
            }
        }

        public override void SetUpAfterLoad()
        {
            // TODO: Load UKS from XML or re-init
            base.SetUpAfterLoad();
        }

        public List<Thing> GetTheUKS()
        {
            // TODO: Return UKS list from handler
            return new List<Thing>();
        }
    }
}