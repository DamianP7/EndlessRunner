using System.Collections.Generic;
using System;
using UnityEngine;


namespace SaveSystem
{
	[Serializable]
	public class Save
	{
		public List<RecordOnLeaderboard> leaderboard;
	}

	[Serializable]
	public class RecordOnLeaderboard
	{
		public int coins;
		public int distance;

		public RecordOnLeaderboard(int coins, int distance)
		{
			this.coins = coins;
			this.distance = distance;
		}
	}
}