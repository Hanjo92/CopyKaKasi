using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
	[SerializeField] private SpriteRenderer line;
	[SerializeField] private float endTime = 1;
	[SerializeField] private float stayTime = 0;

	private int enterCount = 0;
	private Color lineColor;
	private float LineAlpha => stayTime / endTime;

	private void Start()
	{
		lineColor = line.color;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Fruit")) enterCount++;
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Fruit")) enterCount--;
	}

	private void Update()
	{
		if(enterCount > 0)
		{
			stayTime += Time.deltaTime;
			if(stayTime > endTime)
			{
				GameController.Instance.GameEnd();
			}
			else
			{
				lineColor.a = LineAlpha;
				line.color = lineColor;
			}
		}
		else
		{
			if(stayTime != 0)
			{
				stayTime = 0;
				lineColor.a = LineAlpha;
				line.color = lineColor;
			}
		}
	}
}
