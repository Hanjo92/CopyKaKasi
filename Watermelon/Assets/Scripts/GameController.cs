using Almond;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;
using System.Linq;
using System;

public class GameController : MonoSingleton<GameController>
{
    public static GameController Instance => Inst as GameController;

	[SerializeField] private GuideUI guideUI;
	[SerializeField] private GameUI gameUI;
	[SerializeField] private Transform dropPosition;
    [SerializeField] private Fruit fruitPrefab;
	[SerializeField] private ReplayUI replayPopup;

    private Fruit current;
    private GameData gameData;
    private int nextLevel;
    private bool gameEnd = true;
    private int highestScore;
    public int HighestScore => highestScore;
	public int CurrentScore => gameData == null ? 0 : gameData.Score;

	void Start()
    {
		GameStart();
	}

	public void GameStart() => PlayGame().ContinueWith(() => WaittingNextGame()).Forget();
    private async UniTask PlayGame()
    {
		highestScore = PlayerPrefs.GetInt(Defines.HighestScoreKey, 0);
		gameData = new GameData();
        await guideUI.SetupUI();
		SetNextFruitLevel(0);
		current = SimplePool.Instantiate(fruitPrefab, new object[] { nextLevel });
		current.Hanging(dropPosition);
		gameEnd = false;
		//Play Game
		await UniTask.WaitUntil(()=>gameEnd);
		// direction
		var activefruits = FindObjectsOfType<Fruit>().Where((f)=>f.gameObject.activeSelf);
		foreach (var fruit in activefruits)
		{
			fruit.Release();
			await UniTask.Delay(TimeSpan.FromSeconds(Defines.ScaleTime * 0.5f));
		}
	}
    public void GameEnd()
    {
		if(gameEnd)	return;

        gameEnd = true;
        if(gameData.Score > HighestScore)
            PlayerPrefs.SetInt(Defines.HighestScoreKey, gameData.Score);
	}
	private async UniTask WaittingNextGame()
	{
		replayPopup.gameObject.SetActive(true);
		await UniTask.WaitUntil(() => replayPopup.gameObject.activeSelf == false);
		GameStart();
	}
	private void SetNextFruitLevel(int next)
	{
		nextLevel = next;
		gameUI.SetNextFruit(nextLevel);
	}
	public bool Move(float xPos)
	{
        if(gameEnd) return false;

		var movePoint = dropPosition.position;
		movePoint.x = Mathf.Clamp(xPos, Defines.DropRange.x, Defines.DropRange.y);
		dropPosition.position = movePoint;
		return true;
	}
	public void Drop(float xPos)
    {
		if(Move(xPos) == false)
			return;

        // ´ÙÀ½²¨ ¹× ÇöÀç²¨ °»½Å
        current.transform.SetParent(null);
        current.ActiveCollider();
		current = SimplePool.Instantiate(fruitPrefab, new object[]{ nextLevel });
        current.Hanging(dropPosition);

        SetNextFruitLevel(gameData.GetRandomLevel());
    }
    public void Merge(Fruit fruit1, Fruit fruit2)
    {
		if(gameEnd)	return;

		var mergeLevel = fruit1.Level + 1;

		gameData.AddScore(fruit1.Level);
        gameUI.SetScoreText(CurrentScore);

        var position = Vector3.Lerp(fruit1.transform.position, fruit2.transform.position, 0.5f);
        SimplePool.Release(fruit1);
		SimplePool.Release(fruit2);
        
        var up = SimplePool.Instantiate(fruitPrefab, new object[] { mergeLevel, position});
		up.ActiveCollider();
	}
}
