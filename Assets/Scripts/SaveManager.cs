using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using SaveSystem;

public class SaveManager : MonoBehaviour
{
	private static SaveManager instance;
	public static SaveManager Instance
	{
		get
		{
			if (instance == null)
			{
				print("No SaveManager found on the scene!");
			}
			return instance;
		}
	}

	Save save;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this.gameObject);

		DontDestroyOnLoad(this.gameObject);
	}

	private void Start()
	{
		LoadGame();
	}

	public Save GetSave()
	{
		return save;
	}

	public void SaveGame()
	{
		Save save = CreateSaveGameObject();

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
		bf.Serialize(file, save);
		file.Close();

		Debug.Log("Game Saved");
	}

	private Save CreateSaveGameObject()
	{
		return save;
	}

	public void LoadGame()
	{
		Save save = GetSaveObject("/gamesave.save");
		if (save != null)
		{
			this.save = save;
			Debug.Log("Game Loaded");
		}
		else
		{
			this.save = new Save();
			this.save.leaderboard = new List<RecordOnLeaderboard>();
			Debug.Log("No game saved!");
		}
	}

	private Save GetSaveObject(string fileName)
	{
		Debug.Log(Application.persistentDataPath);
		if (File.Exists(Application.persistentDataPath + fileName))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
			Save save = (Save)bf.Deserialize(file);
			file.Close();

			return save;
		}
		return null;
	}

	public void AddNewRecord(int coins, int distance)
	{
		RecordOnLeaderboard record = new RecordOnLeaderboard(coins, distance);
		//if (save.leaderboard.Count == 0)
		//	save.leaderboard.Add(record);
		//else
		{
			int index = save.leaderboard.FindIndex(x => x.distance < distance);
			if (index < 0)
				index = 0;
			save.leaderboard.Insert(index, record);
		}
		SaveGame();
	}
}
