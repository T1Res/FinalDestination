using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAK_Anim : MonoBehaviour
{
	float rotSpeed = 100f;

	Vector3 pos; //현재위치
	float delta = 0.1f; // 좌(우)로 이동가능한 (x)최대값
	float speed = 2.0f; // 이동속도

	void Start()
	{
		pos = transform.position;
	}

	void Update()
	{
		Vector3 v = pos;
		v.y += delta * Mathf.Sin(Time.time * speed);
		transform.position = v;

		transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
	}
}
