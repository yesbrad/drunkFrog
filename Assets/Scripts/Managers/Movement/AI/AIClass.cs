using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AI Class", menuName = "AI Class")]
public class AIClass : ScriptableObject
{
	[Header("Base Stats")]
	[Range(0, 100)]
	public int baseBoardness = 50;

	[Range(0, 100)]
	public int baseHunger = 50;

	[Range(0, 100)]
	public int baseSoberness = 50;

	[Range(0, 100)]
	public int baseThirst = 50;

	[Header("Thresholds (If Stat is higher than this Obtain)")]

	[Range(0, 100)]
	public int obtainingFunThreshold = 50;

	[Range(0, 100)]
	public int obtainingSocializingThreshold = 50;

	[Range(0, 100)]
	public int obtainingWaterThreshold = 50;

	[Range(0, 100)]
	public int obtainingFoodThreshold = 50;

	[Range(0, 100)]
	public int obtainingAlcoholThreshold = 50;

	[Header("Stat Increase Rate (Seconds)")]

	[Range(0, 100)]
	public float boardomIncreaseRate = 60;

	[Range(0, 100)]
	public float thirstIncreaseRate = 60;

	[Range(0, 100)]
	public float hungerIncreaseRate = 60;

	[Range(0, 100)]
	public float sobernessIncreaseRate = 60;
}
