using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScore;
	[SerializeField] private TextMeshProUGUI currentScore;
	[SerializeField] private Button replayButton;

	private void Awake()
	{
		replayButton.onClick.AddListener(() =>
		{
			gameObject.SetActive(false);
		});
	}
	private void OnEnable()
	{
		highScore.text = PlayerPrefs.GetInt("SavedHighScore", 0).ToString();
		currentScore.text = GameController.Instance.CurrentScore.ToString();
	}
}
