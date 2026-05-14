using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MobHPSlider.Common;

namespace MobHPSlider
{
    public class RemoteBossConfig
    {
        public static void Initialize()
        {
            // --- AUTOMATIC VANILLA SCAN ---
            // This loop scans every single vanilla mob in the game and registers it.
            // This ensures every vanilla mob is "known" to the BossDataManager.
            for (int i = 1; i < NPCID.Count; i++)
            {
                // Get the localized name of the vanilla NPC
                string name = Lang.GetNPCNameValue(i);
                
                // Only register if it's a valid name
                if (!string.IsNullOrEmpty(name))
                {
                    // Default to 1.0f (no change) so they can be overridden later
                    BossDataManager.RegisterBossOverride(name, 1.0f);
                }
            }

            // --- SPECIFIC OVERRIDES ---
            // Add your specific HP multipliers below. 
            // These will overwrite the default 1.0f set by the scan above.

            TryRegister("King Slime", 2.0f);
            TryRegister("Eye of Cthulhu", 2.5f);
            TryRegister("Eater of Worlds", 1.5f);
            TryRegister("Brain of Cthulhu", 1.5f);
            TryRegister("Queen Bee", 2.0f);
            TryRegister("Skeletron", 2.2f);
            TryRegister("Wall of Flesh", 3.0f);
            
            // Hardmode Bosses
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
            // Register the override directly. BossDataManager will handle the dictionary update.
            BossDataManager.RegisterBossOverride(bossName, multiplier);
        }
    }
}
