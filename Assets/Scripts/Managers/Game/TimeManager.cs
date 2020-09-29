using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Experimental.GlobalIllumination;

public class TimeManager : MonoBehaviour
{
	[SerializeField]
	private Transform directinalLight;

	[SerializeField]
	private float duriation = 1;

	public float duriationInSeconds { get => duriation * 60; }

	private float time;
	private float startTime;

	public static event Action<float> onTimeChange;
	private Light dirlight;

	private void Awake()
	{
		StartDay();
		dirlight = directinalLight.GetComponent<Light>();
	}

	public void StartDay()
	{
		startTime = Time.time;
	}

	private void Update()
	{
		time = Time.time + startTime;

		if(onTimeChange != null)
			onTimeChange(GetTime());

		dirlight.intensity = GetTime() > 0.5f ? 0 : 1;

		directinalLight.transform.rotation = Quaternion.Lerp(Quaternion.Euler(60, 0 , 0),Quaternion.Euler(180, 0, 0), GetTime() * 2);
	}

	public float GetTime() => time / duriationInSeconds;
}
