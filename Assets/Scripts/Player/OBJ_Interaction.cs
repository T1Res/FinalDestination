using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class OBJ_Interaction : MonoBehaviour
{
	public float raycastDistance = 2.5f;
	public TMP_Text text;
	public GameObject keyBoxUI;

	public bool isSelected;
	public string selectedObject;

	public GameObject PistolOBJ;
	public GameObject AR1OBJ;
	public GameObject AxeOBJ;

	public GameObject AR1Pre;
	public GameObject SMGPre;
	public GameObject AxePre;

	RaycastHit hit;
	Ray ray;

	public WeaponChange WC_OBJ;

	private void Awake()
	{
		//WC_OBJ = GameObject.Find("Player").GetComponent<WeaponChange>();
		Debug.Log(WC_OBJ.name);
	}

	private void FixedUpdate()
	{
		Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red);

		ray = new Ray(transform.position, transform.forward);

		int _layerMask = (-1) - (1 << LayerMask.NameToLayer("Test") | 1 << LayerMask.NameToLayer("HealOBJ") | 1 << LayerMask.NameToLayer("Flame"));

		if (Physics.Raycast(ray, out hit, raycastDistance, _layerMask))
		{
			GameObject hitObject = hit.collider.gameObject;

			if (hitObject != null)
			{
				if (hitObject.tag == "AR1")
				{
					keyBoxUI.SetActive(true);
					text.gameObject.SetActive(true);

					isSelected = true;
					selectedObject = hitObject.tag;

					text.text = "AKM";

					if (Input.GetKeyDown(KeyCode.F))
					{
						weaponSwap(hitObject, hitObject.tag);
					}
				}

				else if (hitObject.tag == "SMG")
				{
					keyBoxUI.SetActive(true);
					text.gameObject.SetActive(true);

					isSelected = true;
					selectedObject = hitObject.tag;

					text.text = "MP7";

					if (Input.GetKeyDown(KeyCode.F))
					{
						weaponSwap(hitObject, hitObject.tag);
					}
				}

				else if (hitObject.tag == "Axe")
				{
					keyBoxUI.SetActive(true);
					text.gameObject.SetActive(true);

					isSelected = true;
					selectedObject = hitObject.tag;

					text.text = "소방도끼";

					if (Input.GetKeyDown(KeyCode.F))
					{
						weaponSwap(hitObject, hitObject.tag);
					}
				}

				else if (hitObject.tag == "AmmoBox")
				{
					keyBoxUI.SetActive(true);
					text.gameObject.SetActive(true);

					isSelected = true;
					selectedObject = hitObject.tag;

					text.text = "탄약 상자";

					if (Input.GetKeyDown(KeyCode.F))
					{
						if (WeaponChange.instance.primaryWeapon == "AR1")
						{
							AmmoFunction.instance.AR1_Capacity = 40;
							AmmoFunction.instance.AR1_Ammo = 400;
						}

						else if (WeaponChange.instance.primaryWeapon == "SMG")
						{
							AmmoFunction.instance.AR1_Capacity = 50;
							AmmoFunction.instance.AR1_Ammo = 500;
						}
					}
				}

				else
				{
					keyBoxUI.gameObject.SetActive(false);
					text.gameObject.SetActive(false);

					isSelected = false;
					selectedObject = null;
				}
			}
		}

		else if (!Physics.Raycast(ray, out hit, raycastDistance))
		{
			keyBoxUI.gameObject.SetActive(false);
			text.gameObject.SetActive(false);

			isSelected = false;
			selectedObject = null;
		}
	}

	private void weaponSwap(GameObject obj, string tag)
	{
		//AR1 주웠을 때
		if (tag == "AR1")
		{
			//현재 주무기가 AR1일 때
			if(WeaponChange.instance.primaryWeapon == "AR1")
			{
				//현재 무기의 Ammo가 부족하다면
				if(AmmoFunction.instance.AR1_Capacity < 40 || AmmoFunction.instance.AR1_Ammo < 400)
				{
					//탄창 재보급
					AmmoFunction.instance.AR1_Capacity = 40;
					AmmoFunction.instance.AR1_Ammo = 400;
					Destroy(obj);
				}
			}

			//현재 주무기가 SMG일 때
			if (WeaponChange.instance.primaryWeapon == "SMG")
			{
				//SMG을 들고 있는 경우
				if (WeaponChange.instance.currentHoldingWeapon == "SMG")
				{
					WeaponChange.instance.primaryWeapon = tag;
					Destroy(obj);
					WeaponChange.instance.SMG_2_AR1();
				}

				//Pistol을 들고 있는 경우
				else if (WeaponChange.instance.currentHoldingWeapon == "Pistol")
				{
					WeaponChange.instance.primaryWeapon = tag;
					Destroy(obj);
					Instantiate(SMGPre, this.gameObject.transform.position, Quaternion.Euler(0, 0, 90));
				}
			}

			//현재 주무기가 Axe일 때
			if (WeaponChange.instance.primaryWeapon == "Axe")
			{
				//Axe을 들고 있는 경우
				if (WeaponChange.instance.currentHoldingWeapon == "Axe" )
				{
					WeaponChange.instance.primaryWeapon = tag;
					Destroy(obj);
					WeaponChange.instance.Axe_2_AR1();
				}

				//Pistol을 들고 있는 경우
				else if (WeaponChange.instance.currentHoldingWeapon == "Pistol")
				{
					WeaponChange.instance.primaryWeapon = tag;
					Destroy(obj);
					GameObject AxePrefab = Instantiate(AxePre, this.gameObject.transform.position, Quaternion.Euler(0, 0, 90));
				}
			}

			//현재 주무기가 없을 때
			if (WeaponChange.instance.primaryWeapon == null)
			{
				WeaponChange.instance.primaryWeapon = tag;
				Destroy(obj);
			}
		}



		//Axe 주웠을 때
		if (tag == "Axe")
		{
			//현재 주무기가 AR1일 때
			if (WeaponChange.instance.primaryWeapon == "AR1")
			{
				//AR1을 들고 있는 경우
				if (WeaponChange.instance.currentHoldingWeapon == "AR1" && WeaponChange.instance.ChangeTrigger)
				{
					WeaponChange.instance.primaryWeapon = tag;
					Destroy(obj);
					WeaponChange.instance.AR1_2_Axe();
				}

				//Pistol을 들고 있는 경우
				if (WeaponChange.instance.currentHoldingWeapon == "Pistol")
				{
					WeaponChange.instance.primaryWeapon = tag;
					Destroy(obj);
					GameObject AR1prefab = Instantiate(AR1Pre, this.gameObject.transform.position, Quaternion.Euler(0, 0, 90));
				}
			}

			//현재 주무기가 SMG일 때
			if (WeaponChange.instance.primaryWeapon == "SMG")
			{
				//SMG을 들고 있는 경우
				if (WeaponChange.instance.currentHoldingWeapon == "SMG")
				{
					WeaponChange.instance.primaryWeapon = tag;
					Destroy(obj);
					WeaponChange.instance.SMG_2_Axe();
				}

				//Pistol을 들고 있는 경우
				if (WeaponChange.instance.currentHoldingWeapon == "Pistol")
				{
					WeaponChange.instance.primaryWeapon = tag;
					Destroy(obj);
					Instantiate(SMGPre, this.gameObject.transform.position, Quaternion.Euler(0, 0, 90));
				}
			}

			//현재 주무기가 없을 때
			if (WeaponChange.instance.primaryWeapon == null)
			{
				WeaponChange.instance.primaryWeapon = tag;
				Destroy(obj);
			}
		}



		//SMG 주웠을 때
		if (tag == "SMG")
		{
			//현재 주무기가 SMG일 때
			if (WeaponChange.instance.primaryWeapon == "SMG")
			{
				//현재 무기의 Ammo가 부족하다면
				if (AmmoFunction.instance.SMG_Capacity < 50 || AmmoFunction.instance.SMG_Ammo < 500)
				{
					//탄창 재보급
					AmmoFunction.instance.SMG_Capacity = 50;
					AmmoFunction.instance.SMG_Ammo = 500;
					Destroy(obj);
				}
			}

			//현재 주무기가 AR1일 때
			if (WeaponChange.instance.primaryWeapon == "AR1")
			{
				//AR1을 들고 있는 경우
				if (WeaponChange.instance.currentHoldingWeapon == "AR1" && WeaponChange.instance.ChangeTrigger)
				{
					WeaponChange.instance.primaryWeapon = tag;
					Destroy(obj);
					WeaponChange.instance.AR1_2_SMG();
				}

				//Pistol을 들고 있는 경우
				if (WeaponChange.instance.currentHoldingWeapon == "Pistol")
				{
					WeaponChange.instance.primaryWeapon = tag;
					Destroy(obj);
					GameObject ar1Prefab = Instantiate(AR1Pre, this.gameObject.transform.position, Quaternion.Euler(0, 0, 90));
				}
			}

			//현재 주무기가 Axe일 때
			if (WeaponChange.instance.primaryWeapon == "Axe")
			{
				//Axe을 들고 있는 경우
				if (WeaponChange.instance.currentHoldingWeapon == "Axe")
				{
					WeaponChange.instance.primaryWeapon = tag;
					Destroy(obj);
					WeaponChange.instance.Axe_2_SMG();
				}

				//Pistol을 들고 있는 경우
				if (WeaponChange.instance.currentHoldingWeapon == "Pistol")
				{
					WeaponChange.instance.primaryWeapon = tag;
					Destroy(obj);
					GameObject AxePrefab = Instantiate(AxePre, this.gameObject.transform.position, Quaternion.Euler(0, 0, 90));
				}
			}

			//현재 주무기가 없을 때
			if (WeaponChange.instance.primaryWeapon == null)
			{
				WeaponChange.instance.primaryWeapon = tag;
				Destroy(obj);
			}
		}
	}	
}
