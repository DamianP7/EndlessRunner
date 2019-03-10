using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	public int value;
	public CoinType coinType;

	private void OnValidate()
	{
		if (coinType == CoinType.Gold)
		{
			value = GameManager.Instance.goldCoinValue;
		}
		else if (coinType == CoinType.Silver)
		{
			value = GameManager.Instance.silverCoinValue;
		}
			gameObject.tag = "Coin";
	}

	public void PickUp()
	{
		GameManager.Instance.Coins += value;
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Collider2D>().enabled = false;
	}

	public void PlaceCoin()
	{
		GetComponent<SpriteRenderer>().enabled = true;
		GetComponent<Collider2D>().enabled = true;
	}
}

public enum CoinType
{
	Silver, Gold
}