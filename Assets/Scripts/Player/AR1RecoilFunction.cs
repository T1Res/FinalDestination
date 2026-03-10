using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AR1RecoilFunction : MonoBehaviour
{
	// Recoil
	private Transform camRecoil;
	private Vector3 recoilKickback;
	private float recoilAmount;

	public static AR1RecoilFunction instance;

	public void Awake()
	{
		camRecoil = GameObject.Find("Recoil").transform;
		recoilKickback = new Vector3(0.02f, 0.05f, -0.15f);
		recoilAmount = 0.1f;

		instance = this;
	}

	private void Update()
	{
		RecoilBack();
	}

	public void Reset()
	{
		camRecoil = GameObject.Find("Recoil").transform;
		recoilKickback = new Vector3(0.02f, 0.05f, -0.15f);
		recoilAmount = 0.1f;
	}

	public void Recoil()
	{
		Vector3 recoilVector = new Vector3(Random.Range(-recoilKickback.x, recoilKickback.x), recoilKickback.y, recoilKickback.z);
		Vector3 recoilCamVector = new Vector3(-recoilVector.y * 400f, recoilVector.x * 200f, 0);

		transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition + recoilVector, recoilAmount / 2f); // position recoil
		camRecoil.localRotation = Quaternion.Slerp(camRecoil.localRotation, Quaternion.Euler(camRecoil.localEulerAngles + recoilCamVector), recoilAmount); // cam recoil
		//Debug.Log("Recoil");
	}

	// back to original position
	private void RecoilBack()
	{
		camRecoil.localRotation = Quaternion.Slerp(camRecoil.localRotation, Quaternion.identity, Time.deltaTime * 2f);
	}
}
