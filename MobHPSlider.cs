using Terraria.ModLoader;

namespace MobHPSlider
{
    /// <summary>
    /// MobHPSlider — adjusts enemy HP via a server-side config slider.
    /// Supports separate boss scaling, town NPC opt-in, and a minimum HP floor.
    /// </summary>
    public class MobHPSlider : Mod
    {
        public override void Load()
        {
            // Fetch and initialize remote boss data (HP overrides, custom stats, etc)
            Common.BossDataManager.Initialize();
        }
    }
}
