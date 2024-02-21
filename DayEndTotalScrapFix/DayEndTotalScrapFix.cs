using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayEndTotalScrapFix
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class DayEndTotalScrapFix : BaseUnityPlugin
    {
        private const string modGUID = "GhostClef.DayEndTotalScrapFix";
        private const string modName = "Day End Total Scrap Fix";
        private const string modVersion = "0.1.0.0";
        private readonly Harmony harmony = new Harmony(modGUID);

        public static DayEndTotalScrapFix Instance;
        internal ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo(modName + " - Version: " + modVersion);

            harmony.PatchAll(typeof(DayEndTotalScrapFix));
            harmony.PatchAll(typeof(ShipLeaveCalculations));
            harmony.PatchAll(typeof(HUDManagerPatch));
        }
    }
}