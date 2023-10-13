using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] private int id;
	[ReadOnly(true)]public string weaponName;
	[SerializeField] private float defaultDemage;
	[SerializeField] private float defaultSpeed;
	[SerializeField] private Transform firePosition;
	[SerializeField] private Bullet bulletPrefab;
	[SerializeField] private float coolTime = 0.5f;
	private float CoolTime
	{
		get
		{
			var result = coolTime;
			if(skillSet != null && skillSet.TryGetValue(SkillType.CoolTime, out var cool))
			{
				result *= 1 - cool?.Value ?? 0;
			}
			result = Mathf.Max(result, Defines.MinimumCooltime);
			return result;
		}
	}
	private float delay = 0;
	protected bool CanFire => delay > CoolTime;
	private Dictionary<SkillType, Skill> skillSet;
	public float CoolTimeRatio => delay / CoolTime;

	public void WeaponUpdate(float deltaTime)
	{
		if(CanFire)
		{
			if(Input.GetMouseButton(0))
			{
				Fire();
				delay = 0;
			}
		}
		else
		{
			delay += deltaTime;
		}
	}

	private void Fire()
	{
		if(bulletPrefab == null)
		{
			Debug.LogWarning("bulletPrefab is null");
			return;
		}
		var demage = defaultDemage * (1 + (0.1f * Player.Inst.GetWeaponLevel(id)));
		if(skillSet != null && skillSet.TryGetValue(SkillType.DamegeUp, out var dmg))
		{
			demage *= 1 + dmg?.Value ?? 0;
		}
		var speed = defaultSpeed;
		if(skillSet != null && skillSet.TryGetValue(SkillType.BulletSpeed, out var spd))
		{
			speed *= 1 + spd?.Value ?? 0;
		}

		var bullet = SimplePool.Instantiate(bulletPrefab, new object[]{demage, speed, skillSet});
		var fireTransform = firePosition == null ? transform : firePosition;
		bullet.transform.SetPositionAndRotation(fireTransform.position, fireTransform.rotation);
	}
	public void SetSkills(Dictionary<SkillType, Skill> _skillSet) => skillSet = _skillSet;
}
