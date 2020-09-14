using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[AddComponentMenu("Item Controller/Timeline Item")]
[RequireComponent(typeof(PlayableDirector))]
public class TimelineItemController : StaticItemController
{
	[Header("Timeline Item")]
	[SerializeField]
	[Tooltip("The length of this needs to be the same MaxPlayers")]
	private List<ItemCharacterSlot> characterPositions = new List<ItemCharacterSlot>();

	[SerializeField]
	private bool isLooping;

	private PlayableDirector director;

	private bool start;

	public override void Init(ItemData newItem, HouseManager manager, CharacterManager characterManager = null)
	{
		base.Init(newItem, manager);
		director = GetComponent<PlayableDirector>();

		if (isLooping)
		{
			director.Play();
		}
	}

	public override void StartInteract(CharacterManager manager, Action onFinishInteraction = null)
	{
		base.StartInteract(manager, onFinishInteraction);

		ItemCharacterSlot slot = GetFreeSlot();

		if(slot == null)
		{
			Debug.LogError("Timeline controller is missing a slot", gameObject);
		}

		manager.Pawn.LockPawn(true);
		manager.Pawn.StartTimline(slot);

		IEnumerable<PlayableBinding> bindings = director.playableAsset.outputs;

		foreach (PlayableBinding play in bindings)
		{   
			if(play.streamName == constants.timelinePlayerName + slot.ID)
			{
				director.SetGenericBinding(play.sourceObject, manager.Pawn.PawnAnimator);
			}
		}

		if (!isLooping && !start)
		{
			director.time = 0;
			director.Play();
			start = true;
		}
	}

	public ItemCharacterSlot GetFreeSlot ()
	{
		foreach (ItemCharacterSlot slot in characterPositions)
		{
			if (slot.Taken == false)
				return slot;
		}

		return null;
	}

	public override void ClearPlayers()
	{
		foreach (ItemCharacterSlot slot in characterPositions)
		{
			slot.Release();
		}

		base.ClearPlayers();
	}

	[ContextMenu("Create Character Slot")]
	private void CreateCharacterStartPosition()
	{
		Transform newCharacterSlot = new GameObject().transform;
		newCharacterSlot.gameObject.name = "CharacterSlot";
		newCharacterSlot.transform.parent = transform;
		characterPositions.Add(newCharacterSlot.gameObject.AddComponent<ItemCharacterSlot>());
	}

	private void Update()
	{
		if (HasOccupant() && start && !isLooping)
		{
			if(director.time >= director.playableAsset.duration - 0.2f)
			{
				ResetPawns();
				start = false;
				EndInteract();
			}
		}
	}

	public override void OnPickup()
	{
		ResetPawns();
		start = false;
		base.OnPickup();
	}

	public override void Validate()
	{
		base.Validate();

		PlayableDirector dir = GetComponent<PlayableDirector>();

		if (dir == null || dir.playableAsset == null)
		{
			Debug.LogError("Timeline Item Controller is missing it Timeline!", gameObject);
		}
		else
		{
			if (dir.playOnAwake)
				dir.playOnAwake = false;
		}

		if(characterPositions.Count <= 0)
		{
			Debug.Log("Missing Character Positions from Timeline COntroller", gameObject);
		}
	}
}
