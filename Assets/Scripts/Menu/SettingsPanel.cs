using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
	[SerializeField] RectTransform panel;
	[SerializeField] float panelSpeed, openedHeight, closedHeight;

	bool panelOpened = false;
	[SerializeField] bool downDirection;

	private void Awake()
	{
		closedHeight = panel.anchoredPosition.y;
		if (downDirection)
		{
			openedHeight = panel.anchoredPosition.y - panel.sizeDelta.y;
		}
		else
			openedHeight = panel.anchoredPosition.y + panel.sizeDelta.y;

		//Debug.Log("openedHeight: " + openedHeight + "	closedHeight: " + closedHeight);
	}

	private void Update()
	{
	//Debug.Log("panel " + '(' + panelOpened + "): " + panel.position);

		if (downDirection)
		{
			if (panelOpened)
			{
				if (panel.anchoredPosition.y > openedHeight)
					panel.position = new Vector3(panel.position.x, panel.position.y - panelSpeed);
			}
			else if (!panelOpened)
			{
				if (panel.anchoredPosition.y < closedHeight)
					panel.position = new Vector3(panel.position.x, panel.position.y + panelSpeed);
			}
		}
		else
		{
			if (panelOpened)
			{
				if (panel.anchoredPosition.y < openedHeight)
					panel.position = new Vector3(panel.position.x, panel.position.y + panelSpeed);
			}
			else if (!panelOpened)
			{
				if (panel.anchoredPosition.y > closedHeight)
					panel.position = new Vector3(panel.position.x, panel.position.y - panelSpeed);
			}
		}
	}

	public void MovePanel()
	{
		panelOpened = !panelOpened;
	}
}
