using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEnable : MonoBehaviour
{
	[SerializeField]
	[Range(0, 1)]
	private float turnOnTime = 0.5f;

	public void Start ()
	{
		gameObject.SetActive(false);

		TimeManager.onTimeChange += (float time) =>
		{
			if(time >= turnOnTime)
			{
				gameObject.SetActive(true);
			}
		};
	}
}
