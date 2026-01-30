using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace ComboBreakFX
{
    internal class Config
    {
        public static Config Instance { get; set; }

        public virtual bool ModToggle { get; set; } = true;
        public virtual bool OnlyCountMiss { get; set; } = true;
        public virtual bool OnlyFullCombo { get; set; } = false;
        public virtual float MinComboBreak { get; set; } = 100f;

    }
}
