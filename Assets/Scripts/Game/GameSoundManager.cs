using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
	private static GameSoundManager instance;
	public static GameSoundManager Instance
	{
		get
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType<GameSoundManager>();
			return instance;
		}
	}

	[SerializeField] AudioSource musicSource, playerSource, coinSource, buttonSource;
	[SerializeField] AudioClip jumpSound, weeSound, deathSound;
	[SerializeField] GameObject disabledMusic, disabledSound;

	bool sound, music;

	private void Awake()
	{
		music = PlayerPrefs.GetInt("Music") > 0 ? true : false;
		sound = PlayerPrefs.GetInt("Sound") > 0 ? true : false;

		if (music)
		{
			StartCoroutine(FadeIn(musicSource, 7));
			disabledMusic.SetActive(false);
		}
		else
			disabledMusic.SetActive(true);
		if (sound)
		{
			disabledSound.SetActive(false);
		}
		disabledSound.SetActive(true);
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

	public void PlayJump()
	{
		if (sound)
		{
			playerSource.clip = jumpSound;
			playerSource.volume = 1;
			playerSource.Play();
		}
	}

	public void PlayWee()
	{
		if (sound)
		{
			playerSource.clip = weeSound;
			playerSource.volume = 0.8f;
			playerSource.Play();
		}
	}

	public void PlayDeath()
	{
		if (sound)
		{
			playerSource.clip = deathSound;
			playerSource.volume = 1;
			playerSource.Play();
		}
	}

	public void PlayCoin()
	{
		if (sound)
		{
			coinSource.Play();
		}
	}

	public void PlayButton()
	{
		if (sound)
		{
			buttonSource.Play();
		}
	}

	IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
	{
		float startVolume = audioSource.volume;
		while (audioSource.volume > 0)
		{
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
			yield return null;
		}
		audioSource.Stop();
	}

	IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
	{
		audioSource.Play();
		audioSource.volume = 0f;
		while (audioSource.volume < 1)
		{
			audioSource.volume += Time.deltaTime / fadeTime;
			yield return null;
		}
	}

}
