using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalRecord : MonoBehaviour
{
	[SerializeField] Text number, coins, distance;

	public int Number
	{
		set
		{
			number.text = value.ToString();
		}
	}

	public int Coins
	{
		set
		{
			coins.text = value.ToString();
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