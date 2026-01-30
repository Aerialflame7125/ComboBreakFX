using System;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.GameplaySetup;
using HarmonyLib;
using UnityEngine;
using Zenject;

namespace ComboBreakFX.UI
{
    internal class ComboBreakFXUISettings : IInitializable, IDisposable
    {
        Config config = Config.Instance;
        readonly GameplaySetup _gameplaySetup;

        [UIValue("ModToggle")]
        public bool ModToggle
        {
            get => config.ModToggle;
            set => config.ModToggle = value;
        }

        [UIValue("OnlyCountMiss")]
        public bool OnlyCountMiss
        {
            get => config.OnlyCountMiss;
            set => config.OnlyCountMiss = value;
        }

        [UIValue("OnlyFullCombo")]
        public bool OnlyFullCombo
        {
            get => config.OnlyFullCombo;
            set => config.OnlyFullCombo = value;
        }

        [UIValue("MinComboBreak")]
        public float MinComboBreak
        {
            get => config.MinComboBreak;
            set => config.MinComboBreak = Mathf.RoundToInt(value);
        }

        public ComboBreakFXUISettings(Config _config, GameplaySetup gameplaySetup)
        {
            config = _config;
            _gameplaySetup = gameplaySetup;
        }

        public void Initialize() => _gameplaySetup.AddTab("ComboBreakFX", "ComboBreakFX.UI.settings.bsml", this);
        public void Dispose() => _gameplaySetup.RemoveTab("ComboBreakFX");
    }
}
