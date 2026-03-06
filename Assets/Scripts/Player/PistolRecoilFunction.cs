using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class PistolRecoilFunction : MonoBehaviour
{
	// Recoil
	public Transform camRecoil;
	public Vector3 recoilKickback;
	public float recoilAmount;

	public static PistolRecoilFunction instance;

	public void Awake()
	{
		instance = this;
	}

	// Update is called once per frame
	private void Update()
	{
		RecoilBack();
	}

	public void Recoil()
	{
		Vector3 recoilVector = new Vector3(Random.Range(-recoilKickback.x, recoilKickback.x), recoilKickback.y, recoilKickback.z);
		Vector3 recoilCamVector = new Vector3(-recoilVector.y * 200f, recoilVector.x * 200f, 0);

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
