using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AI Class", menuName = "AI Class")]
public class AIClass : ScriptableObject
{
	[Header("Base Stats")]
	[Range(0, 100)]
	public int baseFun = 50;

	[Range(0, 100)]
	public int baseHunger = 50;

	[Range(0, 100)]
	public int baseSober = 50;

	[Range(0, 100)]
	public int baseThirst = 50;

	[Header("Balance")]

	[Range(0, 1)]
	public float balanceHavingFun = 0.5f;

	[Range(0, 1)]
	public float balanceSocializing = 0.5f;

	[Range(0, 1)]
	public float balanceGettingWater = 0.5f;

	[Range(0, 1)]
	public float balanceGettingFood = 0.5f;

	[Range(0, 1)]
	public float balanceGettingDrunk = 0.8f;
}
