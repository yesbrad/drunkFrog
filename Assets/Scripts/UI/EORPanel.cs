using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EORPanel : Panel
{
	[SerializeField]
	private Button continueButton;

	private void Awake()
	{

	}

	public override void Toggle(bool toggle)
	{
		base.Toggle(toggle);

		if (toggle)
		{
			EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
		}
	}

	public void ResetGame()
	{
		GameManager.instance.ResetGame();
	}
}
