using System;
using System.Collections.Generic;
using UnityEngine;

public enum AIStatTypes
{
	Hunger,
	Soberness,
	Thirst,
	Fun,
}

public class AIStats
{
	public class Stat
	{
		public AIStatTypes type;
		public int amount;

		public Stat (AIStatTypes type, int amount)
		{
			this.type = type;
			this.amount = amount;
		}
	}

	public List<Stat> stats = new List<Stat>();

	public AIStats(AIClass aiClass)
	{
		stats.Add(new Stat(AIStatTypes.Fun, aiClass.baseFun));
		stats.Add(new Stat(AIStatTypes.Hunger, aiClass.baseHunger));
		stats.Add(new Stat(AIStatTypes.Soberness, aiClass.baseSober));
		stats.Add(new Stat(AIStatTypes.Thirst, aiClass.baseThirst));
	}

	public void Add (AIStatTypes type, int amount)
	{
		for (int i = 0; i < stats.Count; i++)
		{
			if(stats[i].type == type)
			{
				stats[i].amount = Mathf.Clamp(stats[i].amount + Mathf.Abs(amount), 0 , 100);
			}
		}
	}

	public void Remove(AIStatTypes type, int amount)
	{
		for (int i = 0; i < stats.Count; i++)
		{
			if (stats[i].type == type)
			{
				stats[i].amount = Mathf.Clamp(stats[i].amount - Mathf.Abs(amount), 0, 100);
			}
		}
	}

	public float GetStatOdds (AIStatTypes type)
	{
		return GetStat(type).amount / 100;
	}

	public Stat GetStat(AIStatTypes type)
	{
		for (int i = 0; i < stats.Count; i++)
		{
			if (stats[i].type == type)
			{
				return stats[i];
			}
		}

		return null;
	}

}
