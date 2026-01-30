using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ComboBreakFX;
using ComboBreakFX.UI;
using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Attributes;
using SiraUtil.Zenject;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPAConfig = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;


namespace ComboBreakFX
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        internal static Harmony harmony;


        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger, IPAConfig conf, Zenjector zenjector)
        {
            Instance = this;
            Log = logger;
            Log.Info("ComboBreakFX initialized.");
            harmony = new Harmony("idkcode22.BeatSaber.ComboBreakFX");
            var config = conf.Generated<Config>();
            Config.Instance = config; //i fucking hate you
            zenjector.Install(Location.App, Container => Container.BindInstance(config).AsSingle());
            zenjector.Install(Location.Menu, Container => Container.BindInterfacesTo<ComboBreakFXUISettings>().AsSingle());
            harmony.PatchAll(Assembly.GetExecutingAssembly());

        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            //new GameObject("ComboBreakFXController").AddComponent<ComboBreakFXController>();

        }

        [OnExit]
        public void OnApplicationQuit()
        {
            harmony.UnpatchSelf();
            Log.Debug("OnApplicationQuit");

        }
    }
}
