using HarmonyLib;
using TMPro;

namespace DayEndTotalScrapFix
{
	[HarmonyPatch(typeof(HUDManager), "FillEndGameStats")]
	public class HUDManagerPatch
	{
		[HarmonyPostfix]
		public static void FillEndGameStatsPostfix(HUDManager __instance, int scrapCollected)
		{
			float num = scrapCollected + GlobalVariables.RemainingScrapInLevel;
			((TMP_Text)__instance.statsUIElements.quotaDenominator).text = num.ToString();
		}
	}
}