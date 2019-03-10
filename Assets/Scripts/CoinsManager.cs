using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
	[SerializeField] List<GameObject> coinsSilver, coinsGold;
	List<GameObject> usedGold, usedSilver;

	public int chanceForGoldCoin;

	private void OnValidate()
	{
		Debug.Log("	!--> Coins Manager validated");
		coinsSilver = new List<GameObject>();
		coinsGold = new List<GameObject>();
		Transform[] objects = transform.GetChild(0).GetComponentsInChildren<Transform>();
		foreach (Transform item in objects)
		{
			if (item.parent != transform)
				coinsSilver.Add(item.gameObject);
		}
		objects = transform.GetChild(1).GetComponentsInChildren<Transform>();
		foreach (Transform item in objects)
		{
			if (item.parent != transform)
				coinsGold.Add(item.gameObject);
		}
	}

	private void Awake()
	{
		usedGold = new List<GameObject>();
		usedSilver = new List<GameObject>();
	}

	public GameObject UseGoldCoin()
	{
		int index = Random.Range(0, coinsGold.Count);

		usedGold.Add(coinsGold[index]);
		coinsGold.RemoveAt(index);
		coinsGold[coinsGold.Count - 1].GetComponent<Coin>().PlaceCoin();

		return usedGold[usedGold.Count - 1];
	}

	public GameObject UseSilverCoin()
	{
		int index = Random.Range(0, coinsSilver.Count);

		usedSilver.Add(coinsSilver[index]);
		coinsSilver.RemoveAt(index);
		usedSilver[usedSilver.Count - 1].GetComponent<Coin>().PlaceCoin();

		return usedSilver[usedSilver.Count - 1];
	}

	public GameObject UseRandomCoin()
	{
		if (Random.Range(0, 100) < chanceForGoldCoin)
		{
			int index = Random.Range(0, coinsGold.Count);

			usedGold.Add(coinsGold[index]);
			coinsGold.RemoveAt(index);
			coinsGold[coinsGold.Count - 1].GetComponent<Coin>().PlaceCoin();

			return usedGold[usedGold.Count - 1];
		}
		else
		{
			int index = Random.Range(0, coinsSilver.Count);

			usedSilver.Add(coinsSilver[index]);
			coinsSilver.RemoveAt(index);
			usedSilver[usedSilver.Count - 1].GetComponent<Coin>().PlaceCoin();

			return usedSilver[usedSilver.Count - 1];
		}
	}

	public void ReturnGoldCoin(GameObject goldCoin)
	{
		coinsGold.Add(goldCoin);
		usedGold.Remove(goldCoin);
	}

	public void ReturnSilverCoin(GameObject silverCoin)
	{
		coinsSilver.Add(silverCoin);
		usedSilver.Remove(silverCoin);
	}

	public void ReturnRandomCoin(GameObject coin)
	{
		if (coin.GetComponent<Coin>().coinType == CoinType.Gold)
		{
			coinsGold.Add(coin);
			usedGold.Remove(coin);
		}
		else if (coin.GetComponent<Coin>().coinType == CoinType.Silver)
		{
			coinsSilver.Add(coin);
			usedSilver.Remove(coin);
		}
	}

}
