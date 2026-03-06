using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	public float fixedRotation = 0;
	void Update()
	{
		Vector3 eulerAngles = transform.eulerAngles;
		this.gameObject.transform.eulerAngles = new Vector3(fixedRotation, eulerAngles.y, eulerAngles.z);
	}
}
