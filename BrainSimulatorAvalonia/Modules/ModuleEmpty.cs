// BrainSimulatorAvalonia/Modules/ModuleEmpty.cs
// Ported from WPF version. WPF-specific code is omitted or marked as TODO.
using System;
using System.Xml.Serialization;
using UKS;

namespace BrainSimulatorAvalonia.Modules
{
    public class ModuleEmpty : ModuleBase
    {
        // Any public variable you create here will automatically be saved and restored
        // with the network unless you precede it with the [XmlIgnore] directive
        // [XmlIgnore]
        // public int theStatus = 1;

        public override void Fire()
        {
            Init();
            // TODO: Implement UpdateDialog if needed for Avalonia
        }

        public override void Initialize() { }
        public override void SetUpBeforeSave() { }
        public override void SetUpAfterLoad() { }
        public override void UKSInitializedNotification() { }
    }
}
