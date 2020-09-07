using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
	public Panel[] panels;

	public static GameUI instance;

	private void Awake()
	{
		instance = this;
	}

	public void RefreshUI(PanelIDs id)
	{
		foreach (Panel panel in panels)
		{
			panel.Toggle(panel.ID == id);
		}
	}
}
