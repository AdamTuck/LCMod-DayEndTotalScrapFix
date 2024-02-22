using HarmonyLib;
using UnityEngine;

namespace DayEndTotalScrapFix
{
	[HarmonyPatch(typeof(StartOfRound))]
	public class ShipLeaveCalculations
    {
		[HarmonyPatch("ShipLeave")]
		[HarmonyPostfix]
		public static void Postfix()
		{
			GlobalVariables.RemainingScrapInLevel = CalculateRemainingScrapInLevel();
			GlobalVariables.CollectedScrap = CalculateCollectedScrap();
			GlobalVariables.TotalScrapValueInLevel = GlobalVariables.CollectedScrap + GlobalVariables.RemainingScrapInLevel;

			DayEndTotalScrapFix.Instance.mls.LogInfo("DayEndFix - Total Collected Scrap Today: " + GlobalVariables.CollectedScrap);
			DayEndTotalScrapFix.Instance.mls.LogInfo("DayEndFix - Total Remaining Scrap In Level: " + GlobalVariables.RemainingScrapInLevel);
			DayEndTotalScrapFix.Instance.mls.LogInfo("DayEndFix - Total Generated Scrap: " + GlobalVariables.TotalScrapValueInLevel);
		}

		public static int CalculateRemainingScrapInLevel()
		{
			GrabbableObject[] array = Object.FindObjectsOfType<GrabbableObject>();
			int totalRemainingValue = 0;

			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].itemProperties.isScrap && !array[i].isInShipRoom && !array[i].isInElevator && !array[i].scrapPersistedThroughRounds)
				{
					totalRemainingValue += array[i].scrapValue;
					DayEndTotalScrapFix.Instance.mls.LogInfo("Left Behind: " + array[i].name + ", value: " + array[i].scrapValue);
				}
			}
			return totalRemainingValue;
		}

		public static int CalculateCollectedScrap()
		{
			GrabbableObject[] array = Object.FindObjectsOfType<GrabbableObject>();
			int totalCollectedValue = 0;

			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].itemProperties.isScrap && (array[i].isInShipRoom || array[i].isInElevator) && !array[i].scrapPersistedThroughRounds)
				{
					totalCollectedValue += array[i].scrapValue;
					DayEndTotalScrapFix.Instance.mls.LogInfo("Collected today: " + array[i].name + ", value: " + array[i].scrapValue);
				}
			}
			return totalCollectedValue;
		}
	}
}