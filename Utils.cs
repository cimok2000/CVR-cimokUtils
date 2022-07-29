using MelonLoader;
using UnityEngine;
using System.IO;
using ABI.CCK.Scripts;
using ABI_RC.Core;
using ABI_RC.Core.Player;
using ABI.CCK.Components;
using ABI_RC.Core.UI;

namespace cimokUtils
{
    public static class BuildInfo
    {
        public const string Name = "cimokUtils";
        public const string Description = "ChilloutVR useful Utilities";
        public const string Author = "cimok";
        public const string Company = "Cypheria";
        public const string Version = "1.0.0";
        public const string DownloadLink = null;
    }

    //public class MelonLoaderEvents
    //{
    //    public virtual void OnApplicationStart() { }
    //    public virtual void OnUiManagerUnit() { }
    //    public virtual void OnSceneWasLoaded(int buildIndex, string sceneName) { }
    //    public virtual void OnSceneWasInitialized(int buildIndex, string sceneName) { }
    //    public virtual void OnSceneWasUnloaded(int buildIndex, string sceneName) { }
    //    public virtual void OnApplicationQuit() { }

    //    public MelonLoaderEvents() { }
    //}

    public class cimokUtilsMod : MelonMod
    {
        static float TimeAvatarLoaded = -1.0f;
        // Test Stuff
        //public static MelonPreferences_Category ourFirstCategory;
        //public static MelonPreferences_Entry<bool> ourFirstEntry;

        public override void OnApplicationStart()
        {
            //ourFirstCategory = MelonPreferences.CreateCategory("OurFirstCategory");
            //ourFirstEntry = ourFirstCategory.CreateEntry("ourFirstEntry", false);

            LoggerInstance.Msg("Successfully Loaded");
        }

        public override void OnApplicationLateStart()
        {
            LoggerInstance.Msg("OnApplicationLateStart");
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            LoggerInstance.Msg("OnSceneWasLoader: " + buildIndex.ToString() + " | " + sceneName);
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            LoggerInstance.Msg("OnSceneWasInitialized: " + buildIndex.ToString() + " | " + sceneName);
        }

        public override void OnUpdate()
        {
            //if (ourFirstEntry.Value)
            //    return;

            try
            {
                if (TimeAvatarLoaded != PlayerSetup.Instance.timeAvatarLoaded)
                    LoadNewAvatar();

                TimeAvatarLoaded = PlayerSetup.Instance.timeAvatarLoaded;
            } catch {}

            if (Input.GetKeyDown(KeyCode.T))
            {
                LoggerInstance.Msg("T has been PRESSED");
                CohtmlHud.Instance.ViewDropTextImmediate("cimokUtils", "Hello", "World");
                //MoveFile();
            }
        }

        public void LoadNewAvatar()
        {
            //TimeAvatarLoaded, PlayerSetup.Instance.timeAvatarLoaded;
            LoggerInstance.Msg("Avatar loading");
            LoggerInstance.Msg(string.Format("TimeAvatarLoaded: {0} | PlayerSetup.TAL: {1}", TimeAvatarLoaded, PlayerSetup.Instance.timeAvatarLoaded));
            LoggerInstance.Msg(string.Format("In VR?: {0}",PlayerSetup.Instance._inVr));
            LoggerInstance.Msg(string.Format("Avatar: {0}", PlayerSetup.Instance._avatar.name));
        }

        // Test Stuff
        public string MoveFile()
        {
            string[] files = Directory.GetFiles("../ChilloutVR/ChilloutVR_Data/StreamingAssets/Cohtml/UIResources", "*.*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                LoggerInstance.Msg(file);
            }
            return "File Moved";
        }
    }
}
