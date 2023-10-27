using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteArea : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	Vector2 Size => spriteRenderer == null ? Vector2.zero : spriteRenderer.size;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnDrawGizmos()
	{
		if(spriteRenderer == null) return;

		var gizmoPoints = Defines.Directions;

	}
}
