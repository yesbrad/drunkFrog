using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Data", menuName = "Shop Data")]

public class ShopData : ScriptableObject
{
	public ItemData item;
	public int cost = 100;
	public int amount = 50;
}
