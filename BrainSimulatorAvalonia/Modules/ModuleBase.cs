// BrainSimulatorAvalonia/Modules/ModuleBase.cs
// Minimal stub for ModuleBase to support UKS modules
namespace BrainSimulatorAvalonia.Modules
{
    public class ModuleBase
    {
        public string Label { get; set; } = string.Empty;
        public bool allowMultipleDialogs = false;
        public virtual void Fire() { }
        public virtual void Initialize() { }
        public virtual void SetUpBeforeSave() { }
        public virtual void SetUpAfterLoad() { }
        public virtual void SizeChanged() { }
        protected void Init() { }
    }
}