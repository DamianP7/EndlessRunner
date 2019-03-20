using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalRecord : MonoBehaviour
{
	[SerializeField] Text number, playerName, distance;

	public int Number
	{
		set
		{
			number.text = value.ToString();
		}
	}

	public string Name
	{
		set
		{
			playerName.text = value;
		}
	}

	public int Distance
	{
		set
		{
			distance.text = value.ToString() + " m";
		}
	}
}