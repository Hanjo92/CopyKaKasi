using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IPoolObj
{
    [SerializeField] private string monsterName;
    
    [SerializeField] private float speed = 1f;
	[SerializeField] private float maxHP = 10;
	[SerializeField] private float currentHP = 10;
    [SerializeField] private float demage = 50f;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private new Animation animation;
    [SerializeField] private float coolTime = 0.5f;
	private float delay = 0;

	public Action<Monster> onDead = null;
	public string TemplateKey => monsterName;
    public bool IsAlive => currentHP > 0;

	public void Init(object[] param = null)
    {
        transform.rotation = Quaternion.identity;
        onDead = null;
        currentHP = 10;
	}

    public void Release() => SimplePool.Release(this);

    public void Attacked(float demage)
    {
		currentHP -= demage;
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
        if(CheckAttackRange() )
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
