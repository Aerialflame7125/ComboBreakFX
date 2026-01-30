using HarmonyLib;
using ComboBreakFX;
using UnityEngine;

namespace ComboBreakFX.HarmonyPatches
{
    [HarmonyPatch(typeof(ComboController))]

    internal static class ComboBreakDetector
    {
        static bool fullCombo = true;

        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        static void Start(ref int ____combo)
        {
            Plugin.Log.Info("Resetting fullCombo to true");
            fullCombo = true;
        }

        [HarmonyPatch("HandleNoteWasMissed")]
        [HarmonyPrefix]
        static void Miss(NoteController noteController, ref int ____combo)
        {
            Config config = Config.Instance;
            // return if mod is disabled or if the note is a bomb
            Plugin.Log.Info($"HandleNoteWasMissed called OnlyFullCombo = {config.OnlyFullCombo} FullCombo = {fullCombo}");
            if (!config.ModToggle || noteController.noteData.gameplayType == NoteData.GameplayType.Bomb || (config.OnlyFullCombo && !fullCombo)) return;
            if (____combo >= config.MinComboBreak)
            {
                if (config.OnlyFullCombo)
                {
                    fullCombo = false;
                }
                Plugin.Log.Info($"Miss combo break: {____combo}");
            }
        }

        [HarmonyPatch("HandleNoteWasCut")]
        [HarmonyPrefix]
        static void BadCut(in NoteCutInfo noteCutInfo, ref int ____combo)
        {
            Config config = Config.Instance;
            Plugin.Log.Info($"HandleNoteWasCut called OnlyFullCombo = {config.OnlyFullCombo} FullCombo = {fullCombo}");

            if (!config.ModToggle || config.OnlyCountMiss || (config.OnlyFullCombo && !fullCombo)) return;
            if (!noteCutInfo.allIsOK && ____combo >= config.MinComboBreak)
            {

                if (config.OnlyFullCombo)
                {
                    fullCombo = false;
                }
                Plugin.Log.Info($"Bad cut combo break: {____combo}");
            }
        }

        [HarmonyPatch("HandlePlayerHeadDidEnterObstacles")]
        [HarmonyPrefix]
        static void Obstacle(ref int ____combo)
        {
            Config config = Config.Instance;
            Plugin.Log.Info($"HandlePlayerHeadDidEnterObstacles called OnlyFullCombo = {config.OnlyFullCombo} FullCombo = {fullCombo}");
            if (!config.ModToggle || config.OnlyCountMiss || (config.OnlyFullCombo && !fullCombo)) return;
            if (____combo >= config.MinComboBreak)
            {
                if (config.OnlyFullCombo)
                {
                    fullCombo = false;
                }
                Plugin.Log.Info($"Obstacle combo break: {____combo}");
            }
        }
    }
}
