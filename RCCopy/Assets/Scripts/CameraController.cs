using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
	[SerializeField] private float rotateSpeed = 50f;
	private void Awake()
	{
		targetCamera ??= Camera.main;
	}

	public void CameraViewUpdate()
	{
		if(targetCamera == null)
			return;

		if(Input.GetMouseButton(0))
		{
			var euler = transform.eulerAngles;
			var x = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
			var y = -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
			euler.x += y;
			euler.y += x;
			euler.z = 0;
			targetCamera.transform.eulerAngles = euler;
		}
	}
}
