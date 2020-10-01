using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Experimental.GlobalIllumination;

public class TimeManager : MonoBehaviour
{
	public static TimeManager instance;

	[SerializeField]
	private Transform directinalLight;

	[SerializeField]
	private float duriation = 1;

	[Header("Sun Rotation")]
	[SerializeField]
	private Vector3 startSunRotation = new Vector3(60, 20, 0);

	[SerializeField]
	private Vector3 endSunRotation = new Vector3(180, 30, 0);

	public float duriationInSeconds { get => duriation * 60; }

	private float time;
	private float startTime;
	private float finishTime;

	public static event Action<float> onTimeChange;
	private Light dirlight;

	private bool CurrentRound;

	private void Awake()
	{
		instance = this;
		dirlight = directinalLight.GetComponent<Light>();

		// Clear
		onTimeChange = null;
	}

	public void StartDay()
	{
		time = 0;
		startTime = Time.time;
		finishTime = Time.time + duriationInSeconds;
		CurrentRound = true;
	}

	private void Update()
	{
		if (CurrentRound)
		{
			time += Time.deltaTime;

			if(onTimeChange != null)
				onTimeChange(GetTime());

			dirlight.intensity = GetTime() > 0.5f ? 0 : 1;

			directinalLight.transform.rotation = Quaternion.Lerp(Quaternion.Euler(startSunRotation),Quaternion.Euler(endSunRotation), GetTime() * 2);
		
			if(GetTime() >= 1)
			{
				Debug.Log("Finish Round");
				GameManager.instance.EndGame();
				CurrentRound = false;
			}
		}
	}

	public float GetTime() => time / duriationInSeconds;
}
