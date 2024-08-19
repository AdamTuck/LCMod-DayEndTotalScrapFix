using HarmonyLib;
using UnityEngine;

namespace BetterTotalScrap;

[HarmonyPatch(typeof(StartOfRound), "ShipLeave")]
public class ShipleaveCalc
{
	[HarmonyPostfix]
	public static void Postfix()
	{
		GlobalVariables.RemainingScrapInLevel = CalculateRemainingScrapInLevel();
		GlobalVariables.CollectedScrap = CalculateCollectedScrap();
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
}