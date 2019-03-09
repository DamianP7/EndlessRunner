using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceChildren : MonoBehaviour
{
	public float width;

	Vector3 startPos;

#if UNITY_EDITOR
	void OnDrawGizmosSelected()
	{
		if (transform.GetChild(0).GetComponent<Obstacle>() == null)
			return;

		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.red;

		for (int i = 0; i < transform.childCount; i++)
		{
			UnityEditor.Handles.Label(new Vector3(transform.GetChild(i).transform.position.x - 1, transform.GetChild(i).transform.position.y - 0.2f), "LVL: " + transform.GetChild(i).GetComponent<Obstacle>().difficultyLevel, style);
		}
	}

	public void MoveChildren()
	{
		startPos = new Vector3( transform.position.x - ( transform.childCount / 2) * width,  transform.position.y);
		 transform.GetChild(0).transform.position = startPos;
		for (int i = 1; i <  transform.childCount; i++)
		{
			 transform.GetChild(i).transform.position = new Vector3(startPos.x + width * i, startPos.y);
		}
	}
#endif
}

