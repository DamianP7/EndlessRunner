using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

	private void Awake()
	{
		if (PlayerPrefs.GetInt("Sound", -1) == -1)
		{
			PlayerPrefs.SetInt("Sound", 1);
			PlayerPrefs.SetInt("Music", 1);
			PlayerPrefs.Save();
		}
	}

	public void StartGame()
	{
		SceneManager.LoadScene(1);
	}
}
