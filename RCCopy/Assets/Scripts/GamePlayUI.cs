using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private TextMeshProUGUI castleHP;
    [SerializeField] private Image coolTime;
    [SerializeField] private Slider waveProgress;

	private void Awake()
	{
        restartButton.onClick.AddListener(() => { GameController.Instance.Restart(); });
	}

	public void UpdateCastleHP(float hp)
	{
		if(castleHP)
			castleHP.text = hp.ToString("d2");
	}
	public void UpdateWaveProgress(float ratio)
	{
		if(waveProgress)
			waveProgress.value = ratio;
	}
}
