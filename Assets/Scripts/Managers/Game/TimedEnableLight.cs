using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class TimedEnableLight : MonoBehaviour
{
	[SerializeField]
	[Range(0, 1)]
	private float turnOnTime = 0.5f;

	[SerializeField]
	[Range(0, 20)]
	private float turnOnSpeed = 1;

	[SerializeField]
	private float intensity = 1;

	private Light timeLight;

	public void Start ()
	{
		timeLight = GetComponent<Light>();
		timeLight.intensity = 0;

		TimeManager.onTimeChange += (float time) =>
		{
			if(time >= turnOnTime)
			{
				timeLight.intensity = Mathf.Lerp(timeLight.intensity, intensity, turnOnSpeed * Time.deltaTime);
			}
		};
	}
}
