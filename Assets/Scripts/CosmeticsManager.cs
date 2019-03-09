using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsManager : MonoBehaviour
{
	[SerializeField] List<GameObject> cosmeticElements;
	List<GameObject> usedCosmeticElements;

	private void OnValidate()
	{
		Debug.Log("	!--> Cosmetics Manager validated");
		cosmeticElements = new List<GameObject>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform[] objects = transform.GetChild(i).GetComponentsInChildren<Transform>();
			foreach (Transform item in objects)
			{
				if (item.parent != transform)
				cosmeticElements.Add(item.gameObject);
			}
		}
	}

	private void Awake()
	{
		usedCosmeticElements = new List<GameObject>();
	}

	public GameObject UseRandomElement()
	{
		int index = Random.Range(0, cosmeticElements.Count);

		usedCosmeticElements.Add(cosmeticElements[index]);
		cosmeticElements.RemoveAt(index);

		return usedCosmeticElements[usedCosmeticElements.Count - 1];
	}

	public void ReturnElement(GameObject cosmeticsElement)
	{
		cosmeticElements.Add(cosmeticsElement);
		usedCosmeticElements.Remove(cosmeticsElement);
	}

}
