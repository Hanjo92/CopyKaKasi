using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObj
{
	[SerializeField] private LayerMask targetLayer;
    private float speed;
	private float demage;

	public string TemplateKey => "CrossbowBasicArrow";

	private Dictionary<SkillType, Skill> skillSet;

	private bool IsPenetrate => skillSet?.ContainsKey(SkillType.Penetrate) ?? false;

	public void Init(object[] param = null)
	{
		if(param != null && param.Length >= 3)
		{
			demage = (float)param[0];
			speed = (float)param[1];
			skillSet = (Dictionary<SkillType, Skill>)param[2];
		}
		else
		{
			demage = Defines.DefaultBulletDemage;
			speed = Defines.DefaultBulletSpeed;
		}
	}

	public void Release() => SimplePool.Release(this);

	private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.forward;
    }

	private void OnTriggerEnter(Collider other)
	{
		if((1 << other.gameObject.layer & targetLayer.value) > 0)
		{
			if(!other.gameObject.TryGetComponent<Monster>(out var monster))
				return;

			if(monster.IsAlive)
			{
				// 몬스터에 충돌 데미지를 준다.
				monster.Attacked(demage);
				if(IsPenetrate == false)
					Release();
			}
			else
			{
				return;
			}
		}
		if(other.gameObject.layer == LayerMask.NameToLayer("Map"))
		{
			// 맵에 충돌
			Release();
		}
	}
}
