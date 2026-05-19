using Terraria;
using MobHPSlider.Common;

namespace MobHPSlider
{
    public class RemoteBossConfig
    {
        public static void Initialize()
        {
            TryRegister("King Slime", 2.0f);
            TryRegister("Eye of Cthulhu", 2.5f);
            TryRegister("Eater of Worlds", 1.5f);
            TryRegister("Brain of Cthulhu", 1.5f);
            TryRegister("Queen Bee", 2.0f);
            TryRegister("Skeletron", 2.2f);
            TryRegister("Wall of Flesh", 3.0f);
            
            TryRegister("Queen Slime", 2.5f);
            TryRegister("The Destroyer", 2.0f);
            TryRegister("Retinazer", 2.0f);
            TryRegister("Spazmatism", 2.0f);
            TryRegister("Skeletron Prime", 2.2f);
            TryRegister("Plantera", 3.0f);
            TryRegister("Golem", 4.0f);
            TryRegister("Duke Fishron", 2.5f);
            TryRegister("Empress of Light", 2.0f);
            TryRegister("Lunatic Cultist", 2.5f);
            TryRegister("Moon Lord", 5.0f);
        }

        private static void TryRegister(string bossName, float multiplier)
        {
            BossDataManager.RegisterBossOverride(bossName, multiplier);
        }
    }
}
