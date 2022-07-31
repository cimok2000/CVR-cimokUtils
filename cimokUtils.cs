using MelonLoader;
using UnityEngine;
using ABI.CCK.Scripts;
using ABI_RC.Core;
using ABI_RC.Core.Player;
using ABI.CCK.Components;
using ABI_RC.Core.UI;
using ABI_RC.Core.Savior;
using ABI_RC.Core.Networking.IO.Instancing;
using ABI_RC.Core.Networking.IO.Social;
using ABI_RC.Core.InteractionSystem;
using HarmonyLib;
using System;

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

    public class cimokUtilsMod : MelonMod
    {
        static float TimeAvatarLoaded = -1.0f;
        public static MelonPreferences_Entry<bool> playerSelectorEnabled;
        public static MelonPreferences_Entry<bool> playerSelectorAutoShowMenu;
        public static MelonLogger.Instance myLogger;

        public override void OnApplicationStart()
        {
            // Player Selector
            var PlayerSelector = MelonPreferences.CreateCategory("PlayerSelector", "Player Selector");
            playerSelectorEnabled = PlayerSelector.CreateEntry("Enabled", true, "Enable Player Selector");
            playerSelectorAutoShowMenu = PlayerSelector.CreateEntry("AutoShowMenu", true, "Auto Show Menu");

            LoggerInstance.Msg("Successfully Loaded");
        }

        public override void OnApplicationLateStart()
        {
            //LoggerInstance.Msg("OnApplicationLateStart");
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            //LoggerInstance.Msg("OnSceneWasLoader: " + buildIndex.ToString() + " | " + sceneName);
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            //LoggerInstance.Msg("OnSceneWasInitialized: " + buildIndex.ToString() + " | " + sceneName);
        }

        public override void OnUpdate()
        {
            try
            {
                if (TimeAvatarLoaded != PlayerSetup.Instance.timeAvatarLoaded)
                    LoadNewAvatar();

                TimeAvatarLoaded = PlayerSetup.Instance.timeAvatarLoaded;
            } catch { }

            if (Input.GetKeyDown(KeyCode.T) && !MetaPort.Instance.isUsingVr)
            {
                LoggerInstance.Msg("Flying Toggle");
                PlayerSetup.Instance._movementSystem.ToggleFlight();
            }

            //if (Input.GetKeyDown(KeyCode.Y) && !MetaPort.Instance.isUsingVr)
            //{
            //    var playerPosition = PlayerSetup.Instance.transform;
            //    LoggerInstance.Msg($"localPosition: {playerPosition.localPosition} | position: {playerPosition.position}");
            //}

            // Player Selector PC Only: 
            if (playerSelectorEnabled.Value && !MetaPort.Instance.isUsingVr)
                if (Input.GetMouseButton(1) && Input.GetMouseButtonUp(0))
                    DisplayUserDetails();
        }

        public void LoadNewAvatar()
        {
            try
            {
                ViewDropText("Loaded Avatar", $"ID: {MetaPort.Instance.currentAvatarGuid}");
            } catch { }
        }

        public void DisplayUserDetails()
        {
            if (!Physics.Raycast(Camera.current.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) return;
            if (!hit.collider.isTrigger) return;
            var descriptor = hit.collider.GetComponent<PlayerDescriptor>();
            if (descriptor == null) return;
            Users.ShowDetails(descriptor.ownerId);
            if (playerSelectorAutoShowMenu.Value) ViewManager.Instance.UiStateToggle(true);
        }

        public void ViewDropText(string headline, string small)
        {
            try
            {
                CohtmlHud.Instance.ViewDropText(headline, small);
            } catch { LoggerInstance.Error("Failed to print to screen.."); }
        }

        public static void TestInit()
        {
            myLogger.Msg("CVRAppcimokModInstalled");
        }
    }

    [HarmonyPatch(typeof(ViewManager), "RegisterEvents")]
    public class TestPatch
    {
        public static void PostFix(ViewManager __instance)
        {
            __instance.gameMenuView.View.BindCall("CVRAppcimokModInstalled", new Action(cimokUtilsMod.TestInit));
        }
    }
}
