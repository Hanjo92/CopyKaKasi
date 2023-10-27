using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBox : MonoBehaviour
{
	private Camera cam;
	private Camera Cam => cam ??= Camera.main;
	[SerializeField] private float delay = 0.5f;
	float t = 0;
	private bool CanDrop => t > delay;

	private void Update()
	{
		if(CanDrop == false)
			t += Time.deltaTime;
	}

	private void OnMouseDrag()
	{
		var ray = Cam.ScreenPointToRay(Input.mousePosition);
		GameController.Instance.Move(ray.origin.x);
	}
	private void OnMouseUp()
	{
		if(CanDrop == false)
			return;
		var ray = Cam.ScreenPointToRay(Input.mousePosition);
		GameController.Instance.Drop(ray.origin.x);
		t = 0;
	}
}
