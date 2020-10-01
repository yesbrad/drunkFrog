using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : Panel
{
	[SerializeField]
	private Slider timeSlider;

	private void Awake()
	{
		TimeManager.onTimeChange += (time) => OnTimeChange(time);
	}

	public override void Toggle(bool toggle)
	{
		base.Toggle(toggle);
	}

	public void OnTimeChange (float time)
	{
		timeSlider.value = time;
	}
}
