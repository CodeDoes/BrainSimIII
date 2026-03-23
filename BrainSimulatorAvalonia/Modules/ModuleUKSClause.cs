// BrainSimulatorAvalonia/Modules/ModuleUKSClause.cs
// Ported from BrainSimulator/Modules/ModuleUKSClause.cs for Avalonia
using System.Collections.Generic;
using UKS;

namespace BrainSimulatorAvalonia.Modules
{
    public class ModuleUKSClause : ModuleBase
    {
        public ModuleUKSClause() { }

        public override void Fire()
        {
            Init();
            // TODO: Update dialog if needed
        }

        public override void Initialize() { }
        public override void SetUpBeforeSave() { }
        public override void SetUpAfterLoad() { }
        public override void SizeChanged() { }
    }
}