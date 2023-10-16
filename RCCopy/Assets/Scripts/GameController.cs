using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Video;
using System.Threading;

public class GameController : MonoSingleton<GameController>
{
	public static GameController Instance => Inst as GameController;

	[SerializeField] private SkillPanel skillPanel;
	[SerializeField] private GamePlayUI gamePlayUI;
	[SerializeField] private NextGameUI nextGameUI;

    [SerializeField] private CameraController cameraController;
	[SerializeField] private Transform weaponPosition;
    [SerializeField] private List<MonsterWave> monsterWaves = new List<MonsterWave>();
    private Weapon weapon;

	private bool isPaused = false;
	public void Pause() => isPaused = true; 
	public void Resume() => isPaused = false;

	private PlayerStatus playerStatus;
	public float CurrentCastleHP => playerStatus?.GetCurrentHP() ?? 0;
	public bool PlayerDead => CurrentCastleHP <= 0;
	public float CoolTimeRatio => weapon != null ? weapon.CoolTimeRatio : 0;

	public Dictionary<SkillType, Skill> SkillSet => playerStatus?.GetSkillSet() ?? null;
	public void AttackCastle(float demage)
	{
		if(PlayerDead)
		{
			Debug.LogWarning("Player already dead");
			return;
		}
		playerStatus?.Attacked(demage);
		gamePlayUI.UpdateCastleHP(CurrentCastleHP);
	}
	public void AddSkill(SkillType skill)
	{
		if(skill == SkillType.Max)
			return;
		playerStatus.AddSkill(skill);
		weapon.SetSkills(SkillSet);
		skillPanel.gameObject.SetActive(false);
	}

	public List<SkillType> PickRandomSkill(int count = 3)
	{
		var skillList = new List<SkillType>();
		for(SkillType t = SkillType.DamegeUp; t != SkillType.Max; t++)
		{
			if(t == SkillType.Penetrate && SkillSet.ContainsKey(t))
				continue;
			skillList.Add(t);
		}
		skillList = skillList.ShuffleList();

		return skillList.GetRange(0, 3);
	}
	private void OnApplicationFocus(bool focus)
	{
		if(focus == false)
		{
			gamePlayUI.OpenPausedPanel();
		}
	}
	private void OnDestroy()
	{
		cts?.Cancel();
	}
	private CancellationTokenSource cts;
	private void Start()
	{
		GameStart();
	}
	public void GameStart()
	{
		Debug.Log("Start");
		cts = new CancellationTokenSource();
		GameStartTask(cts).ContinueWith(() => WaittingNextGame(PlayerDead == false)).Forget();
	}
	public void Restart()
	{
		Restarting().Forget();
	}
	private async UniTaskVoid Restarting()
	{
		cts.Cancel();
		await UniTask.WaitUntil(() => cts.IsCancellationRequested);
		GameStart();
	}

	private GameObject castleObj = null;

	private async UniTask GameStartTask(CancellationTokenSource cts)
	{
		if(castleObj == null)
		{
			var castlePrefab = Resources.Load<GameObject>("Castle".PrefabPath());
			castleObj = Instantiate(castlePrefab);
			castleObj.transform.position = Vector3.forward * Defines.CastlePosition;
		}

		playerStatus ??= new PlayerStatus();
		playerStatus.Initialize();


		var weaponPrefab = Resources.Load<Weapon>(Player.Inst.SampleWeaponName.PrefabPath());
		weapon = Instantiate(weaponPrefab);
		weapon.transform.SetParent(weaponPosition, false);
		isPaused = false;
		var waveRound = 0;
		try
		{
			for(;waveRound < monsterWaves.Count; waveRound++)
			{
				cameraController.enabled = false;
				weapon.gameObject.SetActive(false);
				skillPanel.gameObject.SetActive(true);
				skillPanel.SetSkillUIs();

				gamePlayUI.gameObject.SetActive(false);
				gamePlayUI.UpdateWaveProgress((waveRound + 1) / (float)monsterWaves.Count);
				await UniTask.WaitUntil(() => skillPanel.gameObject.activeSelf == false, cancellationToken: cts.Token);

				cameraController.enabled = true;
				weapon.gameObject.SetActive(true);
				gamePlayUI.gameObject.SetActive(true);
				monsterWaves[waveRound].WaveInitialize();
				await UniTask.WaitUntil(() => {
					if(isPaused == false)
					{
						gamePlayUI.UpdateCoolTimeProgress(CoolTimeRatio);
						weapon.WeaponUpdate(Time.deltaTime);
						cameraController.CameraViewUpdate();

						monsterWaves[waveRound].UpdateMonsters(Time.deltaTime);
					}

					return monsterWaves[waveRound].WaveEnd || PlayerDead; }, cancellationToken: cts.Token);

				if(PlayerDead)
					break;
			}
		}
		catch(System.Exception e)
		{
			Debug.Log("Cancel" + e.ToString());
			monsterWaves[waveRound].MonsterClear();
		}
	}
	private async UniTask WaittingNextGame(bool isClear)
	{
		nextGameUI.gameObject.SetActive(true);
		nextGameUI.SetTexts(isClear);
		await UniTask.WaitUntil(() => nextGameUI.gameObject.activeSelf == false);
		Restart();
	}
}
