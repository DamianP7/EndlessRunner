using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlaceChildren))]
public class PlaceChildrenEditor : Editor
{
	List<GameObject> children;
	Vector3 startPos;
	float width;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		PlaceChildren myScript = (PlaceChildren)target;
		width = myScript.width;

		if (GUILayout.Button("Place Children"))
		{
			if (myScript.transform.GetChild(0).GetComponent<Obstacle>() == null)
			{
				if (myScript.transform.GetChild(0).GetComponent<PlaceChildren>() == null)
					return;
				else
				{
					startPos = myScript.transform.position;
					for (int i = 0; i < myScript.transform.childCount; i++)
					{
						myScript.transform.GetChild(i).transform.position = new Vector3(startPos.x, startPos.y - width * i);
						myScript.transform.GetChild(i).GetComponent<PlaceChildren>().MoveChildren();
					}
				}
			}
			else
				myScript.MoveChildren();
		}
	}


}
