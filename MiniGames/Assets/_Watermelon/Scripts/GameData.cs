using UnityEngine;

public class GameData
{
	private int highLevel = 0;
	private int score;
	public int Score => score;
	public int GetRandomLevel()
	{
		if(highLevel == 0)
			return 0;

		return Random.Range(0, Mathf.Min(3, highLevel + 1));
	}

	public void AddScore(int level)
	{
		score += (level + 1) * 2;
		highLevel = Mathf.Max(highLevel, level);
	}
}