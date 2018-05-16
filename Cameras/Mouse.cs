using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

	enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivity = 15F;
	
	const float limitY = 90F;

	float rotationY = 0F;

	public bool IsStrateryMode { get; set; } = false;

	void Update ()
	{
		if (Input.GetKey(KeyCode.LeftShift))// GetMouseButtonDown(1))
		{
			IsStrateryMode = true;
			Cursor.lockState = CursorLockMode.None;			
		}
		else
		{
			IsStrateryMode = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
		if (IsStrateryMode)
			return;

		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;

			rotationY += Input.GetAxis("Mouse Y") * sensitivity;
			rotationY = Mathf.Clamp(rotationY, -limitY, limitY);

			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivity;
			rotationY = Mathf.Clamp(rotationY, -limitY, limitY);

			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}
}
