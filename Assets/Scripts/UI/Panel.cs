using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
	[SerializeField]
	private PanelIDs id;

	public PanelIDs ID { get { return id; } }

	public virtual void Toggle(bool toggle)
	{
		gameObject.SetActive(toggle);
	}
}
