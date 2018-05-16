using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 8.0F;
	public float jumpSpeed = 8.0F;
	public int scrollSpeed = 100;
	public KeyCode JumpKey = KeyCode.Space;
	public KeyCode GoDownKey = KeyCode.LeftControl;
	private Vector3 moveDirection = Vector3.zero;
	CharacterController controller;
	void Start()
	{
		controller = GetComponent<CharacterController>();
	}
	void Update()
	{
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") + Input.GetAxis("Mouse ScrollWheel")* scrollSpeed);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;

		if (Input.GetKey(JumpKey))
			moveDirection.y = jumpSpeed;
		if (Input.GetKey(GoDownKey))
			moveDirection.y -= jumpSpeed;

		controller.Move(moveDirection * Time.deltaTime);
	}
}
