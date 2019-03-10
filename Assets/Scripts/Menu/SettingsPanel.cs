using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
	[SerializeField] RectTransform panel;
	[SerializeField] float panelSpeed, openedHeight, closedHeight;

	bool panelOpened = false;
	[SerializeField] bool reversed;

	int dir;

	private void Awake()
	{
	
	}

	private void Update()
	{
		Debug.Log("panel " + '(' + panelOpened + "): " + panel.position);

		if (reversed)
		{
			if (panelOpened)
			{
				if (panel.position.y > openedHeight)
					panel.SetPositionAndRotation(new Vector3(panel.position.x, panel.position.y - panelSpeed), new Quaternion());
			}
			else if (!panelOpened)
			{
				if (panel.position.y < closedHeight)
					panel.SetPositionAndRotation(new Vector3(panel.position.x, panel.position.y + panelSpeed), new Quaternion());
			}
		}
		else
		{
			if (panelOpened)
			{
				if (panel.position.y < openedHeight)
					panel.SetPositionAndRotation(new Vector3(panel.position.x, panel.position.y + panelSpeed * dir), new Quaternion());
			}
			else if (!panelOpened)
			{
				if (panel.position.y > closedHeight)
					panel.SetPositionAndRotation(new Vector3(panel.position.x, panel.position.y - panelSpeed * dir), new Quaternion());
			}
		}
	}

	public void MovePanel()
	{
		panelOpened = !panelOpened;
	}
}
