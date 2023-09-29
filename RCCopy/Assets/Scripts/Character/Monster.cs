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
    
    public Action<Monster> onDead = null;
	public string TemplateKey => monsterName;
    public bool IsAlive => currentHP > 0;

	public void Init()
    {
        transform.rotation = Quaternion.identity;
        onDead = null;
        currentHP = 10;
	}

    public void Release() => SimplePool.Release(this);

    public void SetDemage(float demage)
    {
		currentHP -= demage;
        if(IsAlive == false)
        {
			onDead?.Invoke(this);
            Release();
		}
	}

	// Update is called once per frame
	void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.forward;
    }
}
