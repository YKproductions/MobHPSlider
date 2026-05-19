using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;
using Terraria;
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

        public static string FetchRemoteData(string rawUrl)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(rawUrl);
                request.Method = "GET";
                request.UserAgent = "Terraria-Mod";
                request.Timeout = 5000;
                
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream dataStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
        
        public static void LoadRemoteBossData(string rawUrl)
        {
            string csCode = FetchRemoteData(rawUrl);
            
            if (csCode.StartsWith("Error"))
            {
                LogError($"[BossDataManager] Download failed: {csCode}");
                return;
            }
            
            try
            {
                CSharpCodeProvider provider = new CSharpCodeProvider();
                CompilerParameters parameters = new CompilerParameters();
                
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Core.dll");
                parameters.ReferencedAssemblies.Add(typeof(Main).Assembly.Location);
                parameters.ReferencedAssemblies.Add(typeof(Mod).Assembly.Location);

                parameters.GenerateInMemory = true;
                parameters.GenerateExecutable = false;
                
                CompilerResults results = provider.CompileAssemblyFromSource(parameters, csCode);
                
                if (results.Errors.HasErrors)
                {
                    string errLog = "Compilation errors:\n";
                    foreach (CompilerError error in results.Errors)
                        errLog += $"{error.ErrorText}\n";
                    LogError($"[BossDataManager] {errLog}");
                    return;
                }
                
                Assembly assembly = results.CompiledAssembly;
                Type type = assembly.GetType("MobHPSlider.RemoteBossConfig");
                if (type != null)
                {
                    MethodInfo method = type.GetMethod("Initialize");
                    if (method != null)
                    {
                        method.Invoke(null, null);
                        LogInfo("[BossDataManager] Remote boss data initialized successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                LogError($"[BossDataManager] Execution error: {ex.Message}");
            }
        }

        public static void Initialize()
        {
            try
            {
                string url = "https://raw.githubusercontent.com/YKproductions/MobHPSlider/refs/heads/main/Data/bossdatadp.cs";
                LoadRemoteBossData(url);
            }
            catch (Exception ex)
            {
                LogError($"[BossDataManager] Failed to process remote boss data: {ex.Message}");
            }
        }
    }
}
