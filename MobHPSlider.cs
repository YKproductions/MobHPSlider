using Terraria.ModLoader;

namespace MobHPSlider
{
    public class MobHPSlider : Mod
    {
        public static MobHPSlider Instance { get; private set; }

        public override void Load()
        {
            Instance = this;
            Common.BossDataManager.Initialize();
        }
    }
}
