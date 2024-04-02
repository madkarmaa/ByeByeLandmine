using HarmonyLib;
using UnityEngine;

namespace ByeByeLandmine.Patches
{
    [HarmonyPatch(typeof(Landmine))]
    internal class LandMineBPatch
    {

        [HarmonyPatch(nameof(Landmine.Start))]
        [HarmonyPostfix]
        static void ExplosionSoundPatch(ref AudioClip ___mineDetonate, ref AudioClip ___mineDetonateFar)
        {
            AudioClip newMineDetonate = ByeByeLandmine.Sfx[0];
            ___mineDetonate = newMineDetonate;
            AudioClip newMineDetonateFar = ByeByeLandmine.Sfx[0];
            ___mineDetonateFar = newMineDetonateFar;
        }

    }
}
