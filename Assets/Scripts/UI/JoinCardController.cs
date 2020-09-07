using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinCardController : MonoBehaviour
{
	[SerializeField]
	[Range(0,3)]
	private int playerIndex;

	[SerializeField]
	private GameObject joinContainer;

	[SerializeField]
	private GameObject joinedContainer;

	public void Join()
	{
		joinedContainer.SetActive(true);
		joinContainer.SetActive(false);
	}

	public void Leave()
	{
		joinedContainer.SetActive(false);
		joinContainer.SetActive(true);
	}
}
