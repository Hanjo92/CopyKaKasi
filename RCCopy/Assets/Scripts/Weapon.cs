using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] private Transform firePosition;
	[SerializeField] private Bullet bulletPrefab;
	[SerializeField] private float coolTime = 0.5f;
	private float delay = 0;
	private bool CanFire => delay > coolTime;

	private void Update()
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
			delay += Time.deltaTime;
		}
	}

	private void Fire()
	{
		if(bulletPrefab == null)
		{
			Debug.LogWarning("bulletPrefab is null");
			return;
		}
		var bullet = SimplePool.Instantiate(bulletPrefab);
		var fireTransform = firePosition == null ? transform : firePosition;
		bullet.transform.SetPositionAndRotation(fireTransform.position, fireTransform.rotation);
	}
}
