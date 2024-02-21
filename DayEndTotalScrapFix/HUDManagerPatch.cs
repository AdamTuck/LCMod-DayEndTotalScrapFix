using HarmonyLib;
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
			float newCollected = GlobalVariables.CollectedScrap;
			((TMP_Text)__instance.statsUIElements.quotaNumerator).text = newCollected.ToString();

			float newTotal = GlobalVariables.CollectedScrap + GlobalVariables.RemainingScrapInLevel;
			((TMP_Text)__instance.statsUIElements.quotaDenominator).text = newTotal.ToString();
		}
	}
}