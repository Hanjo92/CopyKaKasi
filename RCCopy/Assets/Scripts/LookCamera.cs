using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    private Camera targetCamera;

	private void Awake()
	{
		targetCamera = Camera.main;
	}
	public void ChangeCamera(Camera camera) => targetCamera = camera;

	private void LateUpdate()
	{
		if (targetCamera == null) return;
		transform.LookAt(targetCamera.transform);
	}
}
