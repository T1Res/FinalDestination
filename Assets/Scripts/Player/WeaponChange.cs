using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.Burst.Intrinsics;

public class WeaponChange : MonoBehaviour
{
	public static WeaponChange instance;

    public GameObject PistolOBJ;
    public GameObject AR1OBJ;
	public GameObject AxeOBJ;
	public GameObject SMGOBJ;

	public GameObject PistolModel;
	public GameObject AR1Model;
	public GameObject AxeModel;
	public GameObject SMGModel;

	public GameObject AR1Pre;
	public GameObject AxePre;
	public GameObject SMGPre;

	public TextMeshProUGUI warnText;
	public TextMeshProUGUI OOAText;

	public bool ChangeTrigger = true;

	public string primaryWeapon;
	public string currentHoldingWeapon;

	Animator AR1Anim;
	Animator PistolAnim;
	Animator SMGAnim;

	private void Awake()
	{
		instance = this;

		primaryWeapon = null;
		currentHoldingWeapon = "Pistol";

		AR1Anim = AR1OBJ.GetComponent<Animator>();
		PistolAnim = PistolOBJ.GetComponent<Animator>();
		SMGAnim = SMGOBJ.GetComponent<Animator>();
	}

	void Update()
	{
		//AR1이 주무기
		if (primaryWeapon == "AR1")
		{
			//현재 들고 있는 무기가 [Pistol]이고 숫자키 [1]번을 눌렀을 때
			if (Input.GetKeyDown(KeyCode.Alpha1) && currentHoldingWeapon == "Pistol")
			{
				//재장전 중이 아니고 트리거가 true일 때 : 권총에서 AR1
				if (!AmmoFunction.instance.isReloading && ChangeTrigger)
				{
					StartCoroutine(Pistol_to_AR1());
				}

				//재장전 중일 때 : 변경 X, 경고 O
				else if (AmmoFunction.instance.isReloading)
				{
					StartCoroutine(Warning());
				}
			}

			//현재 들고 있는 무기가 [AR1]이고 숫자키 [2]번을 눌렀을 때
			else if (Input.GetKeyDown(KeyCode.Alpha2) && currentHoldingWeapon == "AR1")
			{
				//재장전 중이 아니고 트리거가 true일 때 : AR1에서 권총
				if (!AmmoFunction.instance.isReloading && ChangeTrigger)
				{
					StartCoroutine(AR1_to_Pistol());
				}

				//재장전 중일 때 : 변경 X, 경고 O
				else if (AmmoFunction.instance.isReloading)
				{
					StartCoroutine(Warning());
				}
			}
		}

		//SMG가 주무기
		else if (primaryWeapon == "SMG")
		{
			//현재 들고 있는 무기가 [Pistol]이고 숫자키 [1]번을 눌렀을 때
			if (Input.GetKeyDown(KeyCode.Alpha1) && currentHoldingWeapon == "Pistol")
			{
				//재장전 중이 아니고 트리거가 true일 때 : 권총에서 SMG
				if (!AmmoFunction.instance.isReloading && ChangeTrigger)
				{
					StartCoroutine(Pistol_to_SMG());
				}

				//재장전 중일 때 : 변경 X, 경고 O
				else if (AmmoFunction.instance.isReloading)
				{
					StartCoroutine(Warning());
				}
			}

			//현재 들고 있는 무기가 [SMG]이고 숫자키 [2]번을 눌렀을 때
			else if (Input.GetKeyDown(KeyCode.Alpha2) && currentHoldingWeapon == "SMG")
			{
				//재장전 중이 아니고 트리거가 true일 때 : SMG에서 권총
				if (!AmmoFunction.instance.isReloading && ChangeTrigger)
				{
					StartCoroutine(SMG_to_Pistol());
				}

				//재장전 중일 때 : 변경 X, 경고 O
				else if (AmmoFunction.instance.isReloading)
				{
					StartCoroutine(Warning());
				}
			}
		}

		//Axe가 주무기
		else if (primaryWeapon == "Axe")
		{
			//현재 들고 있는 무기가 [Pistol]이고 숫자키 [1]번을 눌렀을 때
			if (Input.GetKeyDown(KeyCode.Alpha1) && currentHoldingWeapon == "Pistol")
			{
				//재장전 중이 아니고 트리거가 true일 때 : 권총에서 Axe
				if (!AmmoFunction.instance.isReloading && ChangeTrigger)
				{
					StartCoroutine(Pistol_to_Axe());
				}

				//재장전 중일 때 : 변경 X, 경고 O
				else if (AmmoFunction.instance.isReloading)
				{
					StartCoroutine(Warning());
				}
			}

			//현재 들고 있는 무기가 [Axe]이고 숫자키 [2]번을 눌렀을 때
			else if (Input.GetKeyDown(KeyCode.Alpha2) && currentHoldingWeapon == "Axe")
			{
				//공격이 가능한 상태이고 트리거가 true일 때: 권총에서 Axe
				if (!AxeFunction.instance.isSwinging && ChangeTrigger)
				{
					StartCoroutine(Axe_to_Pistol());
				}

				//공격 중일 때 : 변경 X, 경고 O
				else if (AxeFunction.instance.isSwinging)
				{
					StartCoroutine(Warning());
				}
			}
		}
	}

	public void AR1_2_SMG()
	{
		if (ChangeTrigger)
		{
			StartCoroutine(AR1_to_SMG());
		}
	}

	public void AR1_2_Axe()
	{
		if (ChangeTrigger)
		{
			StartCoroutine(AR1_to_Axe());
		}
	}

	public void SMG_2_AR1()
	{
		if (ChangeTrigger)
		{
			StartCoroutine(SMG_to_AR1());
		}
	}

	public void SMG_2_Axe()
	{
		if (ChangeTrigger)
		{
			StartCoroutine(SMG_to_Axe());
		}
	}

	public void Axe_2_AR1()
	{
		if(ChangeTrigger)
		{
			StartCoroutine(Axe_to_AR1());
		}
	}

	public void Axe_2_SMG()
	{
		if (ChangeTrigger)
		{
			StartCoroutine(Axe_to_SMG());
		}
	}

	

	//Pistol에서 AR1로
	IEnumerator Pistol_to_AR1()
	{
		ChangeTrigger = false;
		Pistol.instance.shootable = false;
		PistolAnim.SetTrigger("PistolDown");

		//반동 재설정
		Pistol test = GameObject.Find("Pistol Function").GetComponent<Pistol>();
		yield return test.StartCoroutine(test.Rebound());

		yield return new WaitForSecondsRealtime(0.5f);

		PistolOBJ.SetActive(false);

		AR1OBJ.SetActive(true);
		MainAR1.instance.shootable = false;
		AR1Anim.SetBool("AR1Up", true);

		yield return new WaitForSecondsRealtime(0.5f);

		AR1Anim.SetBool("AR1Up", false);

		GetComponentInChildren<PistolRecoilFunction>().enabled = false;
		GetComponentInChildren<AR1RecoilFunction>().enabled = true;

		Pistol.instance.shootable = true;
		MainAR1.instance.shootable = true;

		ChangeTrigger = true;

		currentHoldingWeapon = "AR1";
	}

	//AR1에서 Pistol으로
	IEnumerator AR1_to_Pistol()
	{
		ChangeTrigger = false;
		MainAR1.instance.shootable = false;
		AR1Anim.SetBool("AR1Down", true);

		//반동 재설정
		MainAR1 test = GameObject.Find("AR1Function").GetComponent<MainAR1>();
		yield return test.StartCoroutine(test.Rebound());

		yield return new WaitForSecondsRealtime(0.5f);

		AR1Anim.SetBool("AR1Down", false);
		AR1OBJ.SetActive(false);

		PistolOBJ.SetActive(true);
		Pistol.instance.shootable = false;
		PistolAnim.SetTrigger("PistolUp");

		yield return new WaitForSecondsRealtime(0.5f);

		GetComponentInChildren<PistolRecoilFunction>().enabled = true;
		GetComponentInChildren<AR1RecoilFunction>().enabled = false;

		Pistol.instance.shootable = true;
		MainAR1.instance.shootable = true;
		ChangeTrigger = true;
		currentHoldingWeapon = "Pistol";
	}


	//Pistol에서 SMG로
	IEnumerator Pistol_to_SMG()
	{
		ChangeTrigger = false;
		Pistol.instance.shootable = false;
		PistolAnim.SetTrigger("PistolDown");

		yield return new WaitForSecondsRealtime(0.28f);

		PistolModel.SetActive(false);

		//반동 재설정
		Pistol test = GameObject.Find("Pistol Function").GetComponent<Pistol>();
		yield return test.StartCoroutine(test.Rebound());

		yield return new WaitForSecondsRealtime(0.22f);

		
		PistolOBJ.SetActive(false);

		SMGOBJ.SetActive(true);
		SMG.instance.shootable = false;
		SMGAnim.SetTrigger("SMGUpTest");

		yield return new WaitForSecondsRealtime(0.15f);

		SMGModel.SetActive(true);

		yield return new WaitForSecondsRealtime(0.35f);

		GetComponentInChildren<PistolRecoilFunction>().enabled = false;
		GetComponentInChildren<SMGRecoilFunction>().enabled = true;

		Pistol.instance.shootable = true;
		SMG.instance.shootable = true;

		ChangeTrigger = true;

		currentHoldingWeapon = "SMG";
	}

	//SMG에서 Pistol으로
	IEnumerator SMG_to_Pistol()
	{
		ChangeTrigger = false;
		SMG.instance.shootable = false;
		SMGAnim.SetTrigger("SMGDownTest");

		//반동 재설정
		SMG test = GameObject.Find("SMG_Function").GetComponent<SMG>();
		yield return test.StartCoroutine(test.Rebound());

		yield return new WaitForSecondsRealtime(0.5f);

		SMGModel.SetActive(false);
		SMGOBJ.SetActive(false);

		PistolOBJ.SetActive(true);
		Pistol.instance.shootable = false;
		PistolAnim.SetTrigger("PistolUp");

		yield return new WaitForSecondsRealtime(0.1f);

		PistolModel.SetActive(true);

		yield return new WaitForSecondsRealtime(0.4f);

		GetComponentInChildren<PistolRecoilFunction>().enabled = true;
		GetComponentInChildren<SMGRecoilFunction>().enabled = false;

		Pistol.instance.shootable = true;
		SMG.instance.shootable = true;
		ChangeTrigger = true;
		currentHoldingWeapon = "Pistol";
	}


	//Pistol에서 Axe으로
	IEnumerator Pistol_to_Axe()
	{
		ChangeTrigger = false;
		Pistol.instance.shootable = false;

		yield return new WaitForSecondsRealtime(0.0f);

		GetComponentInChildren<PistolRecoilFunction>().enabled = false;

		//반동 재설정
		Pistol test = GameObject.Find("Pistol Function").GetComponent<Pistol>();
		yield return test.StartCoroutine(test.Rebound());

		Pistol.instance.shootable = true;
		PistolOBJ.SetActive(false);
		AxeOBJ.SetActive(true);
		ChangeTrigger = true;

		currentHoldingWeapon = "Axe";
	}

	//Axe에서 Pistol으로
	IEnumerator Axe_to_Pistol()
	{
		ChangeTrigger = false;
		//AxeFunction.instance.swingAble = false;

		yield return new WaitForSecondsRealtime(0.0f);

		//AxeFunction.instance.swingAble = true;
		PistolOBJ.SetActive(true);
		AxeOBJ.SetActive(false);
		ChangeTrigger = true;

		currentHoldingWeapon = "Pistol";
	}



	//[무기 스위칭] AR1에서 SMG으로
	public IEnumerator AR1_to_SMG()
	{
		ChangeTrigger = false;
		MainAR1.instance.shootable = false;

		yield return new WaitForSecondsRealtime(0.0f);

		GetComponentInChildren<PistolRecoilFunction>().enabled = true;
		GetComponentInChildren<SMGRecoilFunction>().enabled = true;
		GetComponentInChildren<AR1RecoilFunction>().enabled = false;

		//반동 재설정
		MainAR1 test = GameObject.Find("AR1Function").GetComponent<MainAR1>();
		yield return test.StartCoroutine(test.Rebound());

		MainAR1.instance.shootable = true;
		SMGOBJ.SetActive(true);
		AR1OBJ.SetActive(false);
		ChangeTrigger = true;

		GameObject AR1prefab = Instantiate(AR1Pre, this.gameObject.transform.position, Quaternion.Euler(0, 0, 90));

		currentHoldingWeapon = "SMG";
	}

	//[무기 스위칭] AR1에서 Axe으로
	public IEnumerator AR1_to_Axe()
	{
		ChangeTrigger = false;
		MainAR1.instance.shootable = false;

		yield return new WaitForSecondsRealtime(0.0f);

		GetComponentInChildren<PistolRecoilFunction>().enabled = true;
		GetComponentInChildren<AR1RecoilFunction>().enabled = false;

		//반동 재설정
		MainAR1 test = GameObject.Find("AR1Function").GetComponent<MainAR1>();
		yield return test.StartCoroutine(test.Rebound());

		MainAR1.instance.shootable = true;
		AxeOBJ.SetActive(true);
		AR1OBJ.SetActive(false);
		ChangeTrigger = true;

		GameObject AR1prefab = Instantiate(AR1Pre, this.gameObject.transform.position, Quaternion.Euler(0, 0, 90));

		currentHoldingWeapon = "Axe";
	}

	//[무기 스위칭] SMG에서 AR1으로
	public IEnumerator SMG_to_AR1()
	{
		ChangeTrigger = false;
		SMG.instance.shootable = false;

		yield return new WaitForSecondsRealtime(0.0f);

		GetComponentInChildren<PistolRecoilFunction>().enabled = true;
		GetComponentInChildren<SMGRecoilFunction>().enabled = false;
		GetComponentInChildren<AR1RecoilFunction>().enabled = true;

		//반동 재설정
		SMG test = GameObject.Find("SMG_Function").GetComponent<SMG>();
		yield return test.StartCoroutine(test.Rebound());

		SMG.instance.shootable = true;
		SMGOBJ.SetActive(false);
		AR1OBJ.SetActive(true);
		ChangeTrigger = true;

		GameObject SMGprefab = Instantiate(SMGPre, this.gameObject.transform.position, Quaternion.Euler(0, 0, 90));

		currentHoldingWeapon = "AR1";
	}

	//[무기 스위칭] SMG에서 Axe으로
	public IEnumerator SMG_to_Axe()
	{
		ChangeTrigger = false;
		SMG.instance.shootable = false;

		yield return new WaitForSecondsRealtime(0.0f);

		GetComponentInChildren<PistolRecoilFunction>().enabled = true;
		GetComponentInChildren<SMGRecoilFunction>().enabled = false;

		//반동 재설정
		SMG test = GameObject.Find("SMG_Function").GetComponent<SMG>();
		yield return test.StartCoroutine(test.Rebound());

		SMG.instance.shootable = true;
		AxeOBJ.SetActive(true);
		SMGOBJ.SetActive(false);
		ChangeTrigger = true;

		GameObject SMGprefab = Instantiate(SMGPre, this.gameObject.transform.position, Quaternion.Euler(0, 0, 90));

		currentHoldingWeapon = "Axe";
	}

	//[무기 스위칭] Axe에서 AR1으로
	public IEnumerator Axe_to_AR1()
	{
		ChangeTrigger = false;
		AxeFunction.instance.swingAble = false;

		yield return new WaitForSecondsRealtime(0.0f);

		GetComponentInChildren<AR1RecoilFunction>().enabled = true;

		AxeFunction.instance.swingAble = true;
		AxeOBJ.SetActive(false);
		AR1OBJ.SetActive(true);
		ChangeTrigger = true;

		GameObject AxePrefab = Instantiate(AxePre, this.gameObject.transform.position, Quaternion.Euler(0, 0, 90));

		currentHoldingWeapon = "AR1";
	}

	//[무기 스위칭] Axe에서 SMG으로
	public IEnumerator Axe_to_SMG()
	{
		ChangeTrigger = false;
		AxeFunction.instance.swingAble = false;

		yield return new WaitForSecondsRealtime(0.0f);

		GetComponentInChildren<SMGRecoilFunction>().enabled = true;

		AxeFunction.instance.swingAble = true;
		AxeOBJ.SetActive(false);
		SMGOBJ.SetActive(true);
		ChangeTrigger = true;

		GameObject AxePrefab = Instantiate(AxePre, this.gameObject.transform.position, Quaternion.Euler(0, 0, 90));

		currentHoldingWeapon = "SMG";
	}



	IEnumerator Warning()
	{
		warnText.gameObject.SetActive(true);
		yield return new WaitForSecondsRealtime(1.0f);
		warnText.gameObject.SetActive(false);
	}
}
