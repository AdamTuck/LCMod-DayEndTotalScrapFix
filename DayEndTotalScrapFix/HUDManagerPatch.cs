using HarmonyLib;
using System.Collections;
using TMPro;

namespace DayEndTotalScrapFix
{
	[HarmonyPatch(typeof(HUDManager))]
	public class HUDManagerPatch
	{
		[HarmonyPatch("FillEndGameStats")]
		[HarmonyPostfix]
		public static void FillEndGameStatsPostfix(HUDManager __instance)
		{
			int newCollected = GlobalVariables.CollectedScrap;
			((TMP_Text)__instance.statsUIElements.quotaNumerator).text = newCollected.ToString();

			int newTotal = GlobalVariables.TotalScrapValueInLevel;
			((TMP_Text)__instance.statsUIElements.quotaDenominator).text = newTotal.ToString();

			DayEndTotalScrapFix.Instance.mls.LogInfo("Finished setting new scrap text values on stats screen");
		}

		[HarmonyPatch("SetPlayerLevel")]
		[HarmonyPrefix]
		public static void SetPlayerLevelPatch(HUDManager __instance)
		{
			RoundManager.Instance.scrapCollectedInLevel = GlobalVariables.CollectedScrap;
			RoundManager.Instance.totalScrapValueInLevel = GlobalVariables.TotalScrapValueInLevel;

			DayEndTotalScrapFix.Instance.mls.LogInfo("Set scrap collected to: " + RoundManager.Instance.scrapCollectedInLevel);
			DayEndTotalScrapFix.Instance.mls.LogInfo("Set scrap total to: " + RoundManager.Instance.totalScrapValueInLevel);

			float scrapCollectedRatio = RoundManager.Instance.scrapCollectedInLevel / RoundManager.Instance.totalScrapValueInLevel;
			DayEndTotalScrapFix.Instance.mls.LogInfo("Scrap return scaling check: " + scrapCollectedRatio);
		}
	}
}