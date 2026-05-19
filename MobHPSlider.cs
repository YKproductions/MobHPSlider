using Terraria.ModLoader;

namespace MobHPSlider
{
    public class MobHPSlider : Mod
    {
        public override void Load()
        {
            Common.BossDataManager.Initialize();
        }
    }
}
