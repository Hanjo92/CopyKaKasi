using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Watermelon
{
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
			var current = GameController.Instance.CurrentScore;
			currentScore.text = current.ToString();
			var high = GameController.Instance.HighestScore;
			highScore.text = current > high ? current.ToString() : high.ToString();
		}
	}
}