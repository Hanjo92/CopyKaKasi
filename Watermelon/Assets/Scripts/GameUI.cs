using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI currentScore;
	[SerializeField] private TextMeshProUGUI highestScore;
	[SerializeField] private Image nextFruit;

	private int savedHighScore;

	private void Start()
	{
		savedHighScore = PlayerPrefs.GetInt(Defines.HighestScoreKey, 0);
		highestScore.text = savedHighScore.ToString();
		currentScore.text = "0";
	}

	public void SetNextFruit(int level)
	{
		nextFruit.color = FruitColor.GetColor(level);
	}
	public void SetScoreText(int score)
	{
		currentScore.text = score.ToString();
		if(score > savedHighScore)
		{
			highestScore.text = currentScore.text;
		}
	}
}
