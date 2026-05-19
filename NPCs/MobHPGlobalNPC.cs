using Terraria;
using Terraria.ModLoader;

namespace MobHPSlider
{
    public class MobHPGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public int OriginalLifeMax = -1;
        public float LastMultiplier = 1.0f;

        public override void SetDefaults(NPC npc)
        {
            ApplyScaling(npc);
        }

        public override void PostAI(NPC npc)
        {
            var config = MobHPConfig.Instance;
            if (config == null) return;

            float currentMult = GetTargetMultiplier(npc, config);

            if (currentMult != LastMultiplier)
            {
                UpdateLifeScale(npc, currentMult);
            }
        }

        private void ApplyScaling(NPC npc)
        {
            var config = MobHPConfig.Instance;
            if (config == null) return;

            if (OriginalLifeMax == -1)
                OriginalLifeMax = npc.lifeMax;

            float multiplier = GetTargetMultiplier(npc, config);
            
            int newMax = (int)(OriginalLifeMax * multiplier);
            if (newMax < config.MinHP) newMax = config.MinHP;

            npc.lifeMax = newMax;
            npc.life = newMax;
            LastMultiplier = multiplier;
        }

        private void UpdateLifeScale(NPC npc, float newMultiplier)
        {
            var config = MobHPConfig.Instance;
            
            float healthPercent = (float)npc.life / npc.lifeMax;

            int newMax = (int)(OriginalLifeMax * newMultiplier);
            if (newMax < config.MinHP) newMax = config.MinHP;

            npc.lifeMax = newMax;
            
            npc.life = (int)(newMax * healthPercent);
            if (npc.life < 1 && healthPercent > 0) npc.life = 1;

            LastMultiplier = newMultiplier;
        }

        private float GetTargetMultiplier(NPC npc, MobHPConfig config)
        {
            if (npc.boss)
            {
                if (!config.AffectBosses) return 1.0f;
                if (Common.BossDataManager.BossHPOverrides.TryGetValue(npc.TypeName, out float overrideMult))
                    return overrideMult;
                return config.BossHPPercent / 100f;
            }
            
            if (npc.townNPC)
            {
                return config.AffectTownNPCs ? config.HPPercent / 100f : 1.0f;
            }

            return config.HPPercent / 100f;
        }
    }
}
