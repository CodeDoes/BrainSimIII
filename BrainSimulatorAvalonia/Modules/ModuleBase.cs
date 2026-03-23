// BrainSimulatorAvalonia/Modules/ModuleBase.cs
// Ported from WPF version. WPF-specific code is stubbed or marked as TODO.
using System;
using UKS;

namespace BrainSimulatorAvalonia.Modules
{
    public abstract class ModuleBase
    {
        public bool initialized = false;
        public bool isEnabled = true;
        public string Label { get; set; } = string.Empty;
        public bool dlgIsOpen = false;
        public bool allowMultipleDialogs = false;
        public UKS.UKS theUKS = null;

        // TODO: Dialog and UI fields (dlg, dlgPos, dlgSize) omitted for Avalonia

        public ModuleBase()
        {
            string moduleName = this.GetType().Name;
            if (moduleName.StartsWith("Module"))
            {
                Label = moduleName.Substring(6);
            }
        }

        public abstract void Fire();
        public abstract void Initialize();

        public virtual void UKSInitializedNotification() { }
        public void UKSInitialized()
        {
            // TODO: Implement if needed for Avalonia
        }
        public virtual void UKSReloadedNotification() { }
        public void UKSReloaded()
        {
            // TODO: Implement if needed for Avalonia
        }
        public void GetUKS()
        {
            // TODO: Set theUKS from MainWindow if needed
        }

        protected void Init(bool forceInit = false)
        {
            if (initialized && !forceInit) return;
            initialized = true;
            Initialize();
            // TODO: UpdateDialog and dialog logic omitted
        }

        public virtual void SetUpBeforeSave() { }
        public virtual void SetUpAfterLoad() { }
        public virtual void SizeChanged() { }

        // TODO: Dialog, context menu, and event handler methods omitted for Avalonia
    }
}