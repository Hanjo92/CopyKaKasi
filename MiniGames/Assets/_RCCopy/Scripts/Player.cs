using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Almond;

namespace RCCopy
{
	public class Player : Singleton<Player>
	{
		private int level = 1;
		public float CastleHP => Defines.BaseCastleHP + ((level / Defines.IncreaseLevelCount) * Defines.IncreaseHPAmount);

		public string SampleWeaponName => "Crossbow";
		public int GetWeaponLevel(int id) => 1;

		public Player() : base()
		{
			Load();
		}
		~Player()
		{
			Save();
		}

		public void Load()
		{
			level = PlayerPrefs.GetInt("Player_Level", 1);
		}
		public void Save()
		{
			PlayerPrefs.SetInt("Player_Level", level);
		}
	}
}