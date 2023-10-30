using System;
using UnityEngine;
using Almond;

namespace RCCopy
{
	public class Monster : MonoBehaviour, IPoolObj
	{
		protected const string MonsterUIPrefabName = "MonsterUI";
		protected static MonsterUI MonsterUIPrefab = null;

		[SerializeField] private string monsterName;

		[SerializeField] private float speed = 1f;

		[SerializeField] private float maxHP = 10;
		private float currentHP = 10;

		[SerializeField] private float demage = 50f;
		[SerializeField] private float attackRange = 0.5f;

		[SerializeField] private float coolTime = 0.5f;
		private float delay = 0;

		[SerializeField] private new Animation animation;

		[SerializeField] private Transform monsterUIPosition;

		private MonsterUI monsterUI;

		public Action<Monster> onDead = null;
		public string TemplateKey => monsterName;
		public bool IsAlive => currentHP > 0;

		public void Init(object[] param = null)
		{
			transform.rotation = Quaternion.identity;
			onDead = null;
			currentHP = maxHP;

			MonsterUIPrefab = MonsterUIPrefab != null ? MonsterUIPrefab : Resources.Load<MonsterUI>(MonsterUIPrefabName.PrefabPath());

			if(MonsterUIPrefab == null)
			{
				Debug.LogWarning("UI prefab not found");
				return;
			}
			monsterUI = SimplePool.Instantiate(MonsterUIPrefab, new object[] { monsterUIPosition, monsterName });
		}

		public void Release()
		{
			if(monsterUI)
			{
				monsterUI.Release();
			}

			SimplePool.Release(this);
		}

		public void Attacked(float demage)
		{
			currentHP -= demage;
			if(monsterUI)
			{
				monsterUI.UpdateSlider(currentHP / maxHP);
			}

			if(IsAlive == false)
			{
				onDead?.Invoke(this);
				Release();
			}
		}

		protected bool CheckAttackRange() => Defines.CastlePosition - transform.position.z <= attackRange;
		protected bool CanFire => delay > coolTime;

		protected virtual void Attack()
		{
			animation.Play();
			GameController.Instance.AttackCastle(demage);
			delay = 0;
		}

		public void MonsterUpdate(float deltaTime)
		{
			if(CanFire == false)
			{
				delay += deltaTime;
			}
			if(CheckAttackRange())
			{
				if(CanFire)
					Attack();
			}
			else
			{
				transform.position += speed * deltaTime * Vector3.forward;
			}
		}
	}
}