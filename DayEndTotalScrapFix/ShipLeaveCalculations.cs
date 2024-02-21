using HarmonyLib;
using UnityEngine;

namespace DayEndTotalScrapFix
{
	[HarmonyPatch(typeof(StartOfRound), "ShipLeave")]
	public class ShipLeaveCalculations
    {
		[HarmonyPostfix]
		public static void Postfix()
		{
			GlobalVariables.RemainingScrapInLevel = CalculateRemainingScrapInLevel();
			GlobalVariables.CollectedScrap = CalculateCollectedScrap();
			DayEndTotalScrapFix.Instance.mls.LogInfo("DayEndFix - Remaining Scrap In Level: " + GlobalVariables.RemainingScrapInLevel);
			DayEndTotalScrapFix.Instance.mls.LogInfo("DayEndFix - Collected Scrap Today: " + GlobalVariables.CollectedScrap);
		}

		public static int CalculateRemainingScrapInLevel()
		{
			GrabbableObject[] array = Object.FindObjectsOfType<GrabbableObject>();
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].itemProperties.isScrap && !array[i].isInShipRoom && !array[i].isInElevator && !array[i].scrapPersistedThroughRounds)
				{
					num += array[i].scrapValue;
				}
			}
			return num;
		}

		public static int CalculateCollectedScrap()
		{
			GrabbableObject[] array = Object.FindObjectsOfType<GrabbableObject>();
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].itemProperties.isScrap && array[i].isInShipRoom && !array[i].isInElevator && !array[i].scrapPersistedThroughRounds)
				{
					num += array[i].scrapValue;
				}
			}
			return num;
		}

		public static int GetAccuratePlayerCount ()
        {


			return 1;
        }
	}
}