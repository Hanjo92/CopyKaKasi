using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
	[SerializeField] private GameObject pausedPanel;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private TextMeshProUGUI castleHP;
    [SerializeField] private Image coolTime;
    [SerializeField] private Slider waveProgress;

	private void Awake()
	{
        restartButton.onClick.AddListener(() => { GameController.Instance.Restart(); });
		pauseButton.onClick.AddListener(OpenPausedPanel);
		resumeButton.onClick.AddListener(ClosePausedPanel);
	}
	public void OpenPausedPanel()
	{
		pausedPanel.SetActive(true);
		GameController.Instance.Pause();
	}
	public void ClosePausedPanel()
	{
		pausedPanel.SetActive(false);
		GameController.Instance.Resume();
	}

	public void UpdateCastleHP(float hp)
	{
		if(castleHP)
			castleHP.text = hp.ToString("F0");
	}
	public void UpdateCoolTimeProgress(float ratio)
	{
		if(coolTime)
			coolTime.fillAmount = ratio;
	}
	public void UpdateWaveProgress(float ratio)
	{
		if(waveProgress)
			waveProgress.value = ratio;
	}
}
