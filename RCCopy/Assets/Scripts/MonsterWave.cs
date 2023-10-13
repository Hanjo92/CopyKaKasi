using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterWave : MonoBehaviour
{
    [Serializable]
    public class UnitPosition
    {
        public Vector3 position;
        public string prefabName;
    }

    [SerializeField] private List<UnitPosition> unitPositions = new List<UnitPosition>();
    private List<Monster> monsters = new List<Monster>();
    private int aliveMonsterCount = 0;
    public bool WaveEnd => aliveMonsterCount == 0;
    public Action onWaveEnd;

	public void WaveInitialize()
    {
        foreach (var monster in monsters)
        {
            monster.Release();
        }
		monsters.Clear();
        aliveMonsterCount = 0;

		foreach(var unit in unitPositions)
        {
            var monsterPrefab = Resources.Load<Monster>(unit.prefabName.PrefabPath());
            if(monsterPrefab == null )
            {
                Debug.LogWarning($"Prefab not found :: {unit.prefabName}");
                continue;
            }
            var monster = SimplePool.Instantiate(monsterPrefab);
			monster.transform.position = unit.position;
            monster.onDead = MonsterDead;
			monsters.Add(monster);
		}
		aliveMonsterCount = monsters.Count;
	}

    private void MonsterDead(Monster monster)
    {
        aliveMonsterCount--;
	}

    public void MonsterClear()
    {
        foreach(var monster in monsters)
        {
            monster.Release();
        }
    }

    public void UpdateMonsters(float deltaTime)
    {
		foreach(var monster in monsters)
		{
			monster.MonsterUpdate(deltaTime);
		}
	}

	private void OnDrawGizmos()
	{
		foreach (var unit in unitPositions)
        {
            Gizmos.color = Color.green;
            var position = unit.position;
            Gizmos.DrawSphere(position, 0.1f);
            position.y += 0.15f;
            Handles.Label(position, unit.prefabName);
        }
	}
}
