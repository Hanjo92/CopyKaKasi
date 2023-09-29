using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObj
{
    [SerializeField] private float speed = 2f;
	[SerializeField] private float demage = 5f;

	public string TemplateKey => "CrossbowBasicArrow";

	public void Init() {}

	public void Release() => SimplePool.Release(this);

	private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.forward;
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Monster"))
		{
			if(!other.gameObject.TryGetComponent<Monster>(out var monster))
				return;

			if(monster.IsAlive)
			{
				// 몬스터에 충돌 데미지를 준다.
				monster.SetDemage(demage);
				Release();
			}
			else
			{
				return;
			}
		}
		else if(other.gameObject.layer == LayerMask.NameToLayer("Map"))
		{
			// 맵에 충돌
			Release();
		}
	}
}
