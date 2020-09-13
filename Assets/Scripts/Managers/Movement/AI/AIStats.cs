using System;
using System.Collections.Generic;
using UnityEngine;

public enum AIStatTypes
{
	Hunger,
	Soberness,
	Thirst,
	Boardness,
}

public class AIStats
{
	public class Stat
	{
		public AIStatTypes type;
		public int amount;

		public float currentTime;
		public float increaseRate;

		public Stat (AIStatTypes type, int amount, float increaseRate)
		{
			this.type = type;
			this.amount = amount;
			this.increaseRate = increaseRate;
			SetTime();
		}

		public void SetTime()
		{
			currentTime = Time.time;
		}

		public bool CheckTime()
		{
			return currentTime + increaseRate < Time.time;
		}

		public void IncreaseStat()
		{
			amount = Mathf.Clamp(amount + 1, 0, 100);
		}

		public void DecreaseStat()
		{
			amount = Mathf.Clamp(amount - 1, 0, 100);
		}
	}

	public List<Stat> stats = new List<Stat>();

	public AIStats(AIClass aiClass)
	{
		stats.Add(new Stat(AIStatTypes.Boardness, aiClass.baseBoardness, aiClass.boardomIncreaseRate));
		stats.Add(new Stat(AIStatTypes.Hunger, aiClass.baseHunger, aiClass.hungerIncreaseRate));
		stats.Add(new Stat(AIStatTypes.Soberness, aiClass.baseSoberness, aiClass.sobernessIncreaseRate));
		stats.Add(new Stat(AIStatTypes.Thirst, aiClass.baseThirst, aiClass.thirstIncreaseRate));
	}

	public void Add (AIStatTypes type, int amount)
	{
		for (int i = 0; i < stats.Count; i++)
		{
			if(stats[i].type == type)
			{
				stats[i].amount = Mathf.Clamp(stats[i].amount - Mathf.Abs(amount), 0 , 100);
			}
		}
	}

	public void Remove(AIStatTypes type, int amount)
	{
		for (int i = 0; i < stats.Count; i++)
		{
			if (stats[i].type == type)
			{
				stats[i].amount = Mathf.Clamp(stats[i].amount + Mathf.Abs(amount), 0, 100);
			}
		}
	}

	public void TickStats()
	{
		for (int i = 0; i < stats.Count; i++)
		{
			if (stats[i].CheckTime())
			{
				stats[i].IncreaseStat();
				stats[i].SetTime();
			}
		}
	}

	public float GetStatAmount (AIStatTypes type)
	{
		return GetStat(type).amount;
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
