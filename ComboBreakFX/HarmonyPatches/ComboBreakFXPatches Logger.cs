using HarmonyLib;
using ComboBreakFX;
using UnityEngine;

namespace ComboBreakFX.HarmonyPatches
{
    [HarmonyPatch(typeof(ComboController))]
    internal static class ComboBreakDetector
    {
        [HarmonyPatch("HandleNoteWasMissed")]
        [HarmonyPrefix]
        static void Miss(NoteController noteController, ref int ____combo)
        {
            Config config = Config.Instance;
            if (config == null)
            {
                Plugin.Log.Info("return Miss cuz config is null!");
                return;
            }

            if (!config.ModToggle)
            {
                Plugin.Log.Info("return Miss cuz mod disable");
                return;
            }


            if (noteController.noteData.gameplayType == NoteData.GameplayType.Bomb)
            {
                Plugin.Log.Info("return Miss cuz mod bomb");
                return;
            }

            if (____combo >= config.MinComboBreak)
            {
                Plugin.Log.Info($"Miss Miss combo break: {____combo}");
            }
        }

        [HarmonyPatch("HandleNoteWasCut")]
        [HarmonyPrefix]
        static void BadCut(in NoteCutInfo noteCutInfo, ref int ____combo)
        {
            Config config = Config.Instance;
            if (config == null)
            {
                Plugin.Log.Info("return HandleNoteWasCut cuz config is null!");
                return;
            }

            if (!config.ModToggle)
            {
                Plugin.Log.Info("return HandleNoteWasCut cuz mod disable"); 
                return;

            }

            if (config.OnlyCountMiss)
            {
                Plugin.Log.Info("return HandleNoteWasCut cuz mod onlycountmiss");
                return;
            }

            if (!noteCutInfo.allIsOK && ____combo >= config.MinComboBreak)
            {
                Plugin.Log.Info($"Bad cut combo break: {____combo}");
            }
        }

        [HarmonyPatch("HandlePlayerHeadDidEnterObstacles")]
        [HarmonyPrefix]
        static void Obstacle(ref int ____combo)
        {
            Config config = Config.Instance;
            if (config == null)
            {
                Plugin.Log.Info("return HandlePlayerHeadDidEnterObstacles cuz config is null!");
                return;
            }

            if (!config.ModToggle)
            {
                Plugin.Log.Info("return HandlePlayerHeadDidEnterObstacles cuz mod disable");
                return;

            }

            if (config.OnlyCountMiss)
            {
                Plugin.Log.Info("return HandlePlayerHeadDidEnterObstacles cuz mod onlycountmiss");
                return;
            }

            if (____combo >= config.MinComboBreak)
            {
                Plugin.Log.Info($"Obstacle combo break: {____combo}");
            }
        }
    }
}
