using BepInEx;
using HarmonyLib;

namespace BetterTotalScrap;

[BepInPlugin("impulse.BetterTotalScrap", "BetterTotalScrap", "1.1.0")]
public class BetterTotalScrap : BaseUnityPlugin
{
	private const string modGUID = "impulse.BetterTotalScrap";

	private const string modName = "BetterTotalScrap";

	private const string modVersion = "1.1.0";

	private readonly Harmony harmony = new Harmony("impulse.BetterTotalScrap");

	private void Awake()
	{
		harmony.PatchAll(typeof(HUDManagerPatch));
		harmony.PatchAll(typeof(ShipleaveCalc));
	}
}
