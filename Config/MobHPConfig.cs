using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MobHPSlider
{
    public class MobHPConfig : ModConfig
    {
        public static MobHPConfig Instance;

        public override ConfigScope Mode => ConfigScope.ServerSide;


        [Header("HP_Multipliers")]

        [Label("Regular Mob HP %")]
        [Tooltip("Scales the health of all regular enemies as a percentage of their default value.\n" +
                 "10  = 10% (super easy)\n" +
                 "100 = normal\n" +
                 "500 = 500% (brutal)")]
        [Range(10, 500)]
        [DefaultValue(100)]
        [Slider]
        [Increment(5)]
        public int HPPercent { get; set; }

        [Label("Boss HP %")]
        [Tooltip("Scales boss health independently of regular mobs.\n" +
                 "Only applies when 'Affect Bosses' is enabled.\n" +
                 "100 = normal boss HP")]
        [Range(10, 500)]
        [DefaultValue(100)]
        [Slider]
        [Increment(5)]
        public int BossHPPercent { get; set; }


        [Header("Affected_Mobs")]

        [Label("Affect Bosses")]
        [Tooltip("When enabled, boss HP is scaled using the Boss HP % slider above.")]
        [DefaultValue(false)]
        public bool AffectBosses { get; set; }

        [Label("Affect Town NPCs")]
        [Tooltip("When enabled, town NPC HP is also scaled by the regular mob slider.\n" +
                 "Mostly relevant for the Old One's Army defenders.")]
        [DefaultValue(false)]
        public bool AffectTownNPCs { get; set; }


        [Header("Safety")]

        [Label("Minimum HP Floor")]
        [Tooltip("No mob will ever be scaled below this many HP, regardless of the slider.\n" +
                 "Prevents enemies from being one-shot to death by the config itself.\n" +
                 "Set to 1 to disable the floor.")]
        [Range(1, 100)]
        [DefaultValue(1)]
        [Slider]
        public int MinHP { get; set; }
    }
}
