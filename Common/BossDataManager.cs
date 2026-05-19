using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace MobHPSlider.Common
{
    public class BossDataManager
    {
        public static Dictionary<string, float> BossHPOverrides = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase);

        private static void LogInfo(string message)
        {
            if (MobHPSlider.Instance != null && MobHPSlider.Instance.Logger != null)
                MobHPSlider.Instance.Logger.Info(message);
            else
                Console.WriteLine(message);
        }

        private static void LogError(string message)
        {
            if (MobHPSlider.Instance != null && MobHPSlider.Instance.Logger != null)
                MobHPSlider.Instance.Logger.Error(message);
            else
                Console.Error.WriteLine(message);
        }

        public static void RegisterBossOverride(string bossName, float multiplier)
        {
            if (BossHPOverrides.ContainsKey(bossName))
                BossHPOverrides[bossName] = multiplier;
            else
                BossHPOverrides.Add(bossName, multiplier);
            
            LogInfo($"[BossDataManager] Registered override: {bossName} x{multiplier}");
        }

        public static void Initialize()
        {
            try
            {
                RemoteBossConfig.Initialize();
                LogInfo("[BossDataManager] Boss data initialized successfully.");
            }
            catch (Exception ex)
            {
                LogError($"[BossDataManager] Failed to process boss data: {ex.Message}");
            }
        }
    }
}
