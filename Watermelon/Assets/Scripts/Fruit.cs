using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Almond;
using DG.Tweening;

namespace Watermelon
{
	[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D), typeof(Rigidbody2D))]
	public class Fruit : MonoBehaviour, IPoolObj
	{
		public string TemplateKey => "Fruit";
		private int level = 0;
		public int Level => level;

		private SpriteRenderer spriteRenderer;
		private CircleCollider2D circleCollider;
		private new Rigidbody2D rigidbody2D;

		[SerializeField]private SpriteRenderer faceRenderer;

		private void Awake()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			circleCollider = GetComponent<CircleCollider2D>();
			rigidbody2D = GetComponent<Rigidbody2D>();
		}

		public void Init(object[] param = null)
		{
			if(param == null)
			{
				Debug.LogWarning("param is null");
				Release();
				return;
			}
			if(param.Length < 1)
			{
				Debug.LogWarning("param count not match");
				Release();
				return;
			}
			level = (int)param[0];

			// Setup
			transform.DOScale(Vector3.zero, 0);
			var targetScale = Vector3.one * Defines.FruitDefaultSize;
			targetScale *= Mathf.Pow(1 + Defines.FruitScaleStep, level);
			transform.DOScale(targetScale, Defines.ScaleTime);
			
			rigidbody2D.velocity = Vector3.zero;
			circleCollider.enabled = false;

			rigidbody2D.gravityScale = 0;
			spriteRenderer.color = FruitColor.GetColor(level);

			if(param.Length > 1)
				transform.position = (Vector3)param[1];
		}
		public void Hanging(Transform dropPos)
		{
			transform.SetParent(dropPos);
			transform.localPosition = Vector3.zero;
			transform.localEulerAngles = Vector3.zero;
		}
		public void ActiveCollider() 
		{
			circleCollider.enabled = true;
			rigidbody2D.gravityScale = 1;
		}
		public void Release()
		{
			SimplePool.Release(this);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
            if (collision.gameObject.activeSelf == false)
				return;
            var otherFruit = collision.gameObject.GetComponent<Fruit>();
			if(otherFruit == null)
				return;
			if(otherFruit.Level != Level)
				return;

			// merge

			GameController.Instance.Merge(this, otherFruit);
		}
	}
}