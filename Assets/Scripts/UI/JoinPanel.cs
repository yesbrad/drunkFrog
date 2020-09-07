using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JoinPanel : Panel
{
	[SerializeField]
	private JoinCardController[] joinCards;

	[SerializeField]
	private Button playButton;

	private void Awake()
	{
		playButton.interactable = false;
	}

	public override void Toggle(bool toggle)
	{
		base.Toggle(toggle);

		if (toggle)
		{
			PlayerInputManager.instance.EnableJoining();
			PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
			PlayerInputManager.instance.onPlayerLeft += OnPlayerLeft;
		}
		else
		{
			PlayerInputManager.instance.DisableJoining();
			PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
			PlayerInputManager.instance.onPlayerLeft -= OnPlayerLeft;
		}
	}

	public void StartGame()
	{
		GameManager.instance.StartGame();
	}

	public void OnPlayerJoined (PlayerInput input)
	{
		GameManager.instance.players.Add(input);
		joinCards[input.playerIndex].Join();
		playButton.interactable = true;
	}

	public void OnPlayerLeft(PlayerInput input)
	{
		GameManager.instance.players.Remove(input);
		joinCards[input.playerIndex].Leave();

		playButton.interactable = input.playerIndex != 0;
	}
}
