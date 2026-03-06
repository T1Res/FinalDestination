using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraSelfmade : MonoBehaviour
{
	public float movementSpeed = 7f;

	public float rotationSpeed = 1f;//회전속도
	public float smoothness = 10;//관성 값
	private Quaternion targetRotation = Quaternion.identity;

	private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		// 카메라 이동
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
		Vector3 moveAmount = moveDirection * movementSpeed * Time.deltaTime;
		transform.Translate(moveAmount);

		//회전
		Quaternion rot = transform.rotation;
		rot *= Quaternion.Euler(new Vector3(Input.GetAxis("Mouse Y") * rotationSpeed, -Input.GetAxis("Mouse X") * rotationSpeed, 0));

		float x = rot.eulerAngles.x;
		float y = rot.eulerAngles.y;
		targetRotation = Quaternion.Euler(x, y, 0);

		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothness);
	}
}
