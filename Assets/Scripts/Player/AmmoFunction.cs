using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;

public class AmmoFunction : MonoBehaviour
{
	//싱글턴
    public static AmmoFunction instance;

	//리로드
	public TextMeshProUGUI reloadText;
	public bool isReloading;

	//탄약고갈 OOA
	public TextMeshProUGUI outOfAmmoText;
	bool OOATrigger;
	bool OOATrigger2;

	//권총
	public GameObject PistolObject;
    public int Pistol_Capacity;
	bool pistolTrigger;
	public TextMeshProUGUI Pistol_Capacity_UI;
	public GameObject Pistol_Ammunition;

	//AR1
	public GameObject AR1;
	public int AR1_Capacity;
	public int AR1_Ammo;
	bool ar1Trigger;
	public TextMeshProUGUI AR_Capacity_UI;
	public TextMeshProUGUI AR_Ammo_UI;
	public GameObject AR_Ammo_Ammunition;

	//AR1
	public GameObject SMGObject;
	public int SMG_Capacity;
	public int SMG_Ammo;
	bool SMGTrigger;
	public TextMeshProUGUI SMG_Capacity_UI;
	public TextMeshProUGUI SMG_Ammo_UI;
	public GameObject SMG_Ammo_Ammunition;

	Animator AR1reloadAnim;
	Animator PistolreloadAnim;
	Animator SMGreloadAnim;


	//화염병
	public int hasMolotov;
	public GameObject molotovOBJ;
	public GameObject throwPosition;

	bool mDown;

	public Camera followCamera;

	private LineRenderer _lineRenderer; // 라인 렌더러 참조변수
	private bool _isShowingTrajectory; // 궤적 표시 상태


	void GetInput()
	{
		mDown = Input.GetKeyDown(KeyCode.G);
	}

	private void Awake()
	{
		instance = this;



        Pistol_Capacity = 15;
		pistolTrigger = true;

		AR1_Capacity = 40;
        AR1_Ammo = 400;
		ar1Trigger = true;

		SMG_Capacity = 50;
		SMG_Ammo = 500;
		SMGTrigger = true;



		OOATrigger = true;
		OOATrigger2 = true;



		AR1reloadAnim = AR1.GetComponent<Animator>();
		PistolreloadAnim = PistolObject.GetComponent<Animator>();
		SMGreloadAnim = SMGObject.GetComponent<Animator>();



		_lineRenderer = GetComponent<LineRenderer>();
		_lineRenderer.enabled = false;
	}

	void Update()
	{
		GetInput();

		//탄약 오링 사격불가
		if (AR1_Capacity == 0 && AR1_Ammo == 0)
		{
			MainAR1.instance.shootable = false;
		}

		else if (SMG_Capacity == 0 && SMG_Ammo == 0)
		{
			SMG.instance.shootable = false;
		}



		//UI 비활성화
		if (PistolObject.gameObject.activeSelf == false)
		{
			Pistol_Ammunition.gameObject.SetActive(false);
		}
		if (AR1.gameObject.activeSelf == false)
		{
			AR_Ammo_Ammunition.gameObject.SetActive(false);
		}
		if (SMGObject.gameObject.activeSelf == false)
		{
			SMG_Ammo_Ammunition.gameObject.SetActive(false);
		}




		//권총
		if (PistolObject.gameObject.activeSelf == true)
		{
			Pistol_Ammunition.gameObject.SetActive(true);
			Pistol_Capacity_UI.text = Pistol_Capacity.ToString();

			if (Pistol_Capacity == 0)
			{
				if (pistolTrigger == true)
				{
					StartCoroutine(PistolReload());
				}
			}

			else if (Input.GetKeyDown(KeyCode.R))
			{
				if (pistolTrigger == true)
				{
					StartCoroutine(PistolReload());
				}
			}
		}

		//AR1
		else if (AR1.gameObject.activeSelf == true)
		{
			AR_Ammo_Ammunition.gameObject.SetActive(true);
			AR_Capacity_UI.text = AR1_Capacity.ToString(); AR_Ammo_UI.text = AR1_Ammo.ToString();

			//탄약이 남아있을 시
			if (AR1_Ammo > 0)
			{
				if (AR1_Capacity == 0)
				{
					if (ar1Trigger == true)
					{
						StartCoroutine(AR1Reload());
					}
				}

				else if (Input.GetKeyDown(KeyCode.R))
				{
					if (ar1Trigger == true)
					{
						StartCoroutine(AR1Reload());
					}
				}
			}

			//탄약 고갈 시
			else
			{
				if (AR1_Capacity == 0)
				{
					if (ar1Trigger == true)
					{
						if (OOATrigger2)
						{
							StartCoroutine(OOAText());
						}
					}
				}

				else if (Input.GetKeyDown(KeyCode.R))
				{
					if (ar1Trigger == true)
					{
						if (OOATrigger)
						{
							StartCoroutine(OOAText());
						}
					}
				}
			}
		}

		//SMG
		else if (SMGObject.gameObject.activeSelf == true)
		{
			SMG_Ammo_Ammunition.gameObject.SetActive(true);
			SMG_Capacity_UI.text = SMG_Capacity.ToString(); SMG_Ammo_UI.text = SMG_Ammo.ToString();

			//탄약이 남아있을 시
			if (SMG_Ammo > 0)
			{
				if (SMG_Capacity == 0)
				{
					if (SMGTrigger == true)
					{
						StartCoroutine(SMGReload());
					}
				}

				else if (Input.GetKeyDown(KeyCode.R))
				{
					if (SMGTrigger == true)
					{
						StartCoroutine(SMGReload());
					}
				}
			}

			//탄약 고갈 시
			else
			{
				if (SMG_Capacity == 0)
				{
					if (SMGTrigger == true)
					{
						if (OOATrigger2)
						{
							StartCoroutine(OOAText());
						}
					}
				}

				else if (Input.GetKeyDown(KeyCode.R))
				{
					if (SMGTrigger == true)
					{
						if (OOATrigger)
						{
							StartCoroutine(OOAText());
						}
					}
				}
			}
		}

		//화염병
		molotovFunc();
	}

	void molotovFunc()
	{
		if (hasMolotov == 0)
			return;
		if(mDown && !isReloading && WeaponChange.instance.ChangeTrigger)
		{
			GameObject instantMolotov = Instantiate(molotovOBJ, throwPosition.transform.position, followCamera.transform.rotation);

			hasMolotov--;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Molotov" && hasMolotov == 0)
		{
			hasMolotov++;
			Destroy(other.gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Molotov" && hasMolotov == 0)
		{
			hasMolotov++;
			Destroy(other.gameObject);
		}
	}


	IEnumerator PistolReload()
    {
		PistolreloadAnim.SetTrigger("reload");
		isReloading = true;
		pistolTrigger = false;
		reloadText.gameObject.SetActive(true);
		Pistol.instance.shootable = false;

		yield return new WaitForSecondsRealtime(2.2f);

		Pistol_Capacity = 15;
		reloadText.gameObject.SetActive(false);
		Pistol.instance.shootable = true;
		pistolTrigger = true;
		isReloading = false;
	}

	IEnumerator AR1Reload()
	{
		isReloading = true;
		ar1Trigger = false;
		reloadText.gameObject.SetActive(true);
		MainAR1.instance.shootable = false;
		AR1reloadAnim.SetBool("reloadAnim", true);

		yield return new WaitForSecondsRealtime(3.5f);

		if(AR1_Ammo > 40)
		{
			AR1_Ammo -= (40 - AR1_Capacity);
			AR1_Capacity = 40;
		}
		else
		{
			AR1_Capacity = AR1_Ammo;
			AR1_Ammo = 0;
		}
		
		reloadText.gameObject.SetActive(false);
		MainAR1.instance.shootable = true;
		ar1Trigger = true;
		isReloading = false;
		AR1reloadAnim.SetBool("reloadAnim", false);
	}

	IEnumerator SMGReload()
	{
		isReloading = true;
		SMGTrigger = false;
		reloadText.gameObject.SetActive(true);
		SMG.instance.shootable = false;
		SMGreloadAnim.SetTrigger("SMGReload");

		yield return new WaitForSecondsRealtime(3.0f);

		if (SMG_Ammo > 50)
		{
			SMG_Ammo -= (50 - SMG_Capacity);
			SMG_Capacity = 50;
		}
		else
		{
			SMG_Capacity = SMG_Ammo;
			SMG_Ammo = 0;
		}

		reloadText.gameObject.SetActive(false);
		SMG.instance.shootable = true;
		SMGTrigger = true;
		isReloading = false;
		//AR1reloadAnim.SetBool("reloadAnim", false);
	}

	IEnumerator OOAText()
	{
		OOATrigger = false;
		OOATrigger2 = false;
		outOfAmmoText.gameObject.SetActive(true);

		yield return new WaitForSecondsRealtime(1.0f);

		outOfAmmoText.gameObject.SetActive(false);
		OOATrigger = true;
	}
}
