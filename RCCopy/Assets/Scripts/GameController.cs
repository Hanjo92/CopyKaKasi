using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameController : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Weapon weapon;
	[SerializeField] private GameObject startButton;
    [SerializeField] private List<MonsterWave> monsterWaves = new List<MonsterWave>();

	private void Start()
	{
		GameStart().Forget();
	}

	private async UniTask GameStart()
	{
		foreach (var wave in monsterWaves)
		{
			cameraController.enabled = false;
			weapon.gameObject.SetActive(false);
			startButton.SetActive(true);
			await UniTask.WaitUntil(() => startButton.activeSelf == false);

			cameraController.enabled = true;
			weapon.gameObject.SetActive(true);
			wave.WaveInitialize();
			await UniTask.WaitUntil(() => wave.WaveEnd);
		}
	}
}
