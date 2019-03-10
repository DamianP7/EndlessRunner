using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	[SerializeField] AudioClip menuSong;
	[SerializeField] AudioSource buttonSource, musicSource;
	[SerializeField] GameObject disabledMusic, disabledSound;

	bool sound, music;

	private void Awake()
	{
		if (PlayerPrefs.GetInt("Sound") > 0)
		{
			sound = true;
			disabledSound.SetActive(false);
		}
		else
		{
			sound = false;
			disabledSound.SetActive(true);
		}
		if (PlayerPrefs.GetInt("Music") > 0)
		{
			music = true;
			musicSource.Play();
			disabledMusic.SetActive(false);
		}
		else
		{
			music = false;
			disabledMusic.SetActive(true);
		}
	}

	public void ToggleMusic()
	{
		if (music)
		{
			musicSource.Pause();
			music = false;
			disabledMusic.SetActive(true);
			PlayerPrefs.SetInt("Music", 0);
		}
		else
		{
			musicSource.Play();
			music = true;
			disabledMusic.SetActive(false);
			PlayerPrefs.SetInt("Music", 1);
		}
		PlayerPrefs.Save();
	}

	public void ToggleSound()
	{
		if (sound)
		{
			sound = false;
			disabledSound.SetActive(true);
			PlayerPrefs.SetInt("Sound", 0);
		}
		else
		{
			sound = true;
			disabledSound.SetActive(false);
			PlayerPrefs.SetInt("Sound", 1);
		}
		PlayerPrefs.Save();
	}

	public void PlayButton()
	{
		if (sound)
		{
			buttonSource.Play();
		}
	}
}
