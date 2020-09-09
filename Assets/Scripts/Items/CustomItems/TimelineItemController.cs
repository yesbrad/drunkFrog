using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

[AddComponentMenu("Item Controller/Timeline Item")]
[RequireComponent(typeof(PlayableDirector))]
public class TimelineItemController : StaticItemController
{
	[Header("Timeline Item")]
	[SerializeField]
	private Transform characterPosition;

	private PlayableDirector director;
	private bool playing;

	public override void Init(Item newItem, CharacterManager manager)
	{
		base.Init(newItem, manager);
		director = GetComponent<PlayableDirector>();
	}

	public override void StartInteract(CharacterManager manager, Action onFinishInteraction = null)
	{
		base.StartInteract(manager, onFinishInteraction);

		manager.Pawn.LockPawn(true);
		manager.Pawn.SetPosition(characterPosition.position);
		manager.Pawn.SetRotation(characterPosition.eulerAngles);

		IEnumerable<PlayableBinding> bindings = director.playableAsset.outputs;

		foreach (PlayableBinding play in bindings)
		{
			if(play.streamName == constants.timelinePlayerName)
			{
				director.SetGenericBinding(play.sourceObject, manager.Pawn.PawnAnimator);
			}
		}

		director.Play();
		playing = true;
	}

	private void Update()
	{
		if (playing)
		{
			if(director.time >= director.playableAsset.duration - 0.1f)
			{
				LastUsedCharacter.Pawn.LockPawn(false);
				LastUsedCharacter.Pawn.SetRotation(Vector3.zero);
				playing = false;
				EndInteract();
				Debug.Log("Timeline End");
			}
		}
	}

	public override void Validate()
	{
		base.Validate();
		
		if (director == null || director.playableAsset == null)
		{
			Debug.LogError("Timeline Item Controller is missing it Timeline!", gameObject);
		}

		if(characterPosition == null)
		{
			Debug.Log("Missing Character Position from Timeline COntroller", gameObject);
		}
	}
}
