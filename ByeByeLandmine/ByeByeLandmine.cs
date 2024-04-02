using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace ByeByeLandmine
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class ByeByeLandmine : BaseUnityPlugin
    {
        public static ByeByeLandmine Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }
        internal static List<AudioClip> Sfx { get; private set; } = null!;
        internal static AssetBundle Bundle { get; private set; } = null!;

        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            Patch();

            if (LoadAudioFile()) Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
            else Unpatch();
        }

        internal static void Patch()
        {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            Logger.LogDebug("Patching...");

            Harmony.PatchAll();

            Logger.LogDebug("Finished patching!");
        }

        internal static void Unpatch()
        {
            Logger.LogDebug("Unpatching...");

            Harmony?.UnpatchSelf();

            Logger.LogDebug("Finished unpatching!");
        }

        internal static bool LoadAudioFile()
        {
            Sfx = [];

            string location = Instance.Info.Location;
            location = location.TrimEnd((MyPluginInfo.PLUGIN_GUID + ".dll").ToCharArray());

            Bundle = AssetBundle.LoadFromFile(location + "assets/byebye");

            if (Bundle != null)
            {
                Sfx = [.. Bundle.LoadAllAssets<AudioClip>()];
                Logger.LogInfo("Successfully loaded the audio file");
                return true;
            }
            else
            {
                Logger.LogError("Failed to load audio file");
                return false;
            }
        }
    }
}
