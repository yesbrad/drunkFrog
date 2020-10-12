using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner Settings", menuName = "AI Spawner Settings")]
public class AISpawnerSettings : ScriptableObject
{
	[Header("Pencil Spawner")]
	public AIClass[] aiClasses;

	[Range(1, 10)]
	public int groupSizeMin = 1;

	[Range(1, 10)]
	public int groupSizeMax = 5;

	[Range(1, 10)]
	public int groupRateMin = 1;

	[Range(1, 10)]
	public int groupRateMax = 10;

	[Range(1, 300)]
	public int partyLimit = 200;

	[Range(0, 1)]
	public float partyTime = 0.8f;

}
