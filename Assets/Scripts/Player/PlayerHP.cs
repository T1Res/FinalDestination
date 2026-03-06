using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
	public Slider HPBar;
	public float currentHP;
    public float maxHP;

	public Slider buffBar;
	public float currentBuff;
	public float maxBuff;

	public Image FAK_UI;
	public GameObject FAK_ProgressBar;

	public Image Pills_UI;
	public GameObject Pills_ProgressBar;

	public bool isBuffed;

	public static PlayerHP instance;

	private int FAK_Amount;
	private int FAK_Amount_Fake;
	public bool isFAKUsing;

	private int Pills_Amount;
	private int Pills_Amount_Fake;
	public bool isPillsUsing;

	public GameObject pistol;
	public GameObject AR1;
	public GameObject SMGOBJ;
	public GameObject Axe;

	Animator AR1Anim;
	Animator PistolAnim;
	Animator SMGAnim;

	private void Awake()
	{
		instance = this;

		currentHP = 100;
		maxHP = 100;

		currentBuff = 0;
		maxBuff = 60f;

		FAK_Amount = 0;
		Pills_Amount = 0;

		isFAKUsing = false;
		isPillsUsing = false;

		isBuffed = false;

		AR1Anim = AR1.GetComponent<Animator>();
		PistolAnim = pistol.GetComponent<Animator>();
		SMGAnim = SMGOBJ.GetComponent<Animator>();
	}

	public void CheckHp()
	{
		if (HPBar != null)
			HPBar.value = currentHP / maxHP;
	}

	public void CheckBuff()
	{
		if(currentBuff <= 0)
			buffBar.gameObject.SetActive(false);

		else if (currentBuff > 0)
			buffBar.gameObject.SetActive(true);



		if (buffBar != null) 
			buffBar.value = currentBuff / maxBuff;

		if(currentHP > maxHP)
			currentHP = maxHP;
	}

	public void Damage(float damage)
	{
		if (maxHP == 0 || currentHP <= 0)
			return;

		currentHP -= damage;

		CheckHp();

		if (currentHP <= 0)
		{
			Debug.Log("사망");
		}
	}

	private void FAK_Function()
	{
        if (Input.GetKeyDown(KeyCode.Q))
		{
			if(currentHP < 75 && FAK_Amount_Fake > 0 && !isPillsUsing && !AmmoFunction.instance.isReloading)
			{
				if(WeaponChange.instance.currentHoldingWeapon == "Pistol" && !Pistol.instance.isShooting)
					StartCoroutine(FAK_USE());
				else if (WeaponChange.instance.currentHoldingWeapon == "AR1" && !MainAR1.instance.isShooting)
					StartCoroutine(FAK_USE());
				else if (WeaponChange.instance.currentHoldingWeapon == "SMG" && !SMG.instance.isShooting)
					StartCoroutine(FAK_USE());
				else if (WeaponChange.instance.currentHoldingWeapon == "Axe" && !AxeFunction.instance.isSwinging)
					StartCoroutine(FAK_USE());
			}

			else
			{
				//구상 사용 불가 UI 뜨게 하기
			}
		}
	}

	private void Pills_Function()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (Pills_Amount_Fake > 0 && !isFAKUsing && !AmmoFunction.instance.isReloading && !isBuffed)
			{
				if (WeaponChange.instance.currentHoldingWeapon == "Pistol" && !Pistol.instance.isShooting)
					StartCoroutine(Pills_USE());
				else if (WeaponChange.instance.currentHoldingWeapon == "AR1" && !MainAR1.instance.isShooting)
					StartCoroutine(Pills_USE());
				else if (WeaponChange.instance.currentHoldingWeapon == "SMG" && !SMG.instance.isShooting)
					StartCoroutine(Pills_USE());
				else if (WeaponChange.instance.currentHoldingWeapon == "Axe" && !AxeFunction.instance.isSwinging)
					StartCoroutine(Pills_USE());
			}
		}
	}

    void Update()
    {
        CheckHp();
		CheckBuff();
		FAK_Function();
		Pills_Function();

		/*
		//임시 체력 깎기
		if (Input.GetKeyDown(KeyCode.O))
		{
			currentHP -= 20;
		}
		*/

		#region 구급상자 UI 
		if (FAK_Amount > 0)
		{
			FAK_UI.gameObject.SetActive(true);
		}

		else if(FAK_Amount == 0)
		{
			FAK_UI.gameObject.SetActive(false);
		}
		#endregion


		#region 진통제 UI
		if (Pills_Amount > 0)
		{
			Pills_UI.gameObject.SetActive(true);
		}

		else if (Pills_Amount == 0)
		{
			Pills_UI.gameObject.SetActive(false);
		}
		#endregion
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "FAK")
		{
			if (FAK_Amount == 0)
			{
				FAK_Amount += 1;
				FAK_Amount_Fake += 1;
				Destroy(other.gameObject);
			}
		}

		if (other.gameObject.tag == "Pills")
		{
			if (Pills_Amount == 0)
			{
				Pills_Amount += 1;
				Pills_Amount_Fake += 1;
				Destroy(other.gameObject);
			}
		}
	}

	IEnumerator FAK_USE()
	{
		WeaponChange.instance.ChangeTrigger = false;
		isFAKUsing = true;
		FAK_Amount_Fake -= 1;
		FirstPersonController.instance.walkSpeed = 2f;
		FAK_ProgressBar.gameObject.SetActive(true);

		//pistol 처리
		if (pistol.gameObject.activeSelf == true)
		{
			Pistol.instance.shootable = false;
			PistolAnim.SetTrigger("PistolDown");

			//반동 재설정
			Pistol test = GameObject.Find("Pistol Function").GetComponent<Pistol>();
			yield return test.StartCoroutine(test.Rebound());

			yield return new WaitForSecondsRealtime(0.3f);

			pistol.gameObject.SetActive(false);

			yield return new WaitForSecondsRealtime(6f);

			FAK_Amount -= 1;
			currentHP = 75;
			FirstPersonController.instance.walkSpeed = 5f;

			pistol.gameObject.SetActive(true);
			PistolAnim.SetTrigger("PistolUp");

			yield return new WaitForSecondsRealtime(0.3f);

			Pistol.instance.shootable = true;
			WeaponChange.instance.ChangeTrigger = true;
			isFAKUsing = false;
		}

		//AR1 처리
		else if (AR1.gameObject.activeSelf == true)
		{
			MainAR1.instance.shootable = false;
			AR1Anim.SetBool("AR1Down", true);

			//반동 재설정
			MainAR1 test = GameObject.Find("AR1Function").GetComponent<MainAR1>();
			yield return test.StartCoroutine(test.Rebound());

			yield return new WaitForSecondsRealtime(0.3f);

			AR1Anim.SetBool("AR1Down", false);

			AR1.gameObject.SetActive(false);

			yield return new WaitForSecondsRealtime(6f);

			FAK_Amount -= 1;
			currentHP = 75;
			FirstPersonController.instance.walkSpeed = 5f;

			AR1.gameObject.SetActive(true);
			AR1Anim.SetBool("AR1Up", true);

			yield return new WaitForSecondsRealtime(0.3f);

			AR1Anim.SetBool("AR1Up", false);

			MainAR1.instance.shootable = true;
			WeaponChange.instance.ChangeTrigger = true;
			isFAKUsing = false;
		}

		//SMG 처리
		else if (SMGOBJ.gameObject.activeSelf == true)
		{
			SMG.instance.shootable = false;
			SMGAnim.SetTrigger("SMGDownTest");

			//반동 재설정
			SMG test = GameObject.Find("SMG_Function").GetComponent<SMG>();
			yield return test.StartCoroutine(test.Rebound());

			yield return new WaitForSecondsRealtime(0.3f);

			SMGOBJ.gameObject.SetActive(false);

			yield return new WaitForSecondsRealtime(6f);

			FAK_Amount -= 1;
			currentHP = 75;
			FirstPersonController.instance.walkSpeed = 5f;

			SMGOBJ.gameObject.SetActive(true);
			SMGAnim.SetTrigger("SMGUpTest");

			yield return new WaitForSecondsRealtime(0.3f);

			SMG.instance.shootable = true;
			WeaponChange.instance.ChangeTrigger = true;
			isFAKUsing = false;
		}

		//Axe 처리
		else if (Axe.gameObject.activeSelf == true)
		{
			AxeFunction.instance.swingAble = false;
			Axe.gameObject.SetActive(false);

			yield return new WaitForSecondsRealtime(6f);

			FAK_Amount -= 1;
			currentHP = 75;
			FirstPersonController.instance.walkSpeed = 5f;
			AxeFunction.instance.swingAble = true;
			Axe.gameObject.SetActive(true);
			isFAKUsing = false;
			WeaponChange.instance.ChangeTrigger = true;
		}
	}

	IEnumerator Pills_USE()
	{
		isBuffed = true;
		WeaponChange.instance.ChangeTrigger = false;
		isPillsUsing = true;
		Pills_Amount_Fake -= 1;
		FirstPersonController.instance.walkSpeed = 2f;
		Pills_ProgressBar.gameObject.SetActive(true);

		//pistol 처리
		if (pistol.gameObject.activeSelf == true)
		{
			Pistol.instance.shootable = false;
			PistolAnim.SetTrigger("PistolDown");

			Pistol test = GameObject.Find("Pistol Function").GetComponent<Pistol>();
			yield return test.StartCoroutine(test.Rebound());

			yield return new WaitForSecondsRealtime(0.3f);

			pistol.gameObject.SetActive(false);

			yield return new WaitForSecondsRealtime(3f);

			Pills_Amount -= 1;
			currentBuff = 3600;
			buffBar.value = 60;
			StartCoroutine(BuffTimer());
			FirstPersonController.instance.walkSpeed = 6.5f;

			pistol.gameObject.SetActive(true);
			PistolAnim.SetTrigger("PistolUp");

			yield return new WaitForSecondsRealtime(0.3f);

			Pistol.instance.shootable = true;
			WeaponChange.instance.ChangeTrigger = true;
			isPillsUsing = false;

			for (int i = 0; i < 6; i++)
			{
				FirstPersonController.instance.walkSpeed = 6.5f;
				currentHP += 7;
				yield return new WaitForSecondsRealtime(10f);
				FirstPersonController.instance.walkSpeed = 5f;
			}
			isBuffed = false;
		}

		//AR1 처리
		else if (AR1.gameObject.activeSelf == true)
		{
			MainAR1.instance.shootable = false;
			AR1Anim.SetBool("AR1Down", true);

			MainAR1 test = GameObject.Find("AR1Function").GetComponent<MainAR1>();
			yield return test.StartCoroutine(test.Rebound());

			yield return new WaitForSecondsRealtime(0.3f);

			AR1Anim.SetBool("AR1Down", false);
			AR1.gameObject.SetActive(false);

			yield return new WaitForSecondsRealtime(3f);

			Pills_Amount -= 1;
			currentBuff = 3600;
			buffBar.value = 60;
			StartCoroutine(BuffTimer());
			FirstPersonController.instance.walkSpeed = 6.5f;

			AR1.gameObject.SetActive(true);
			AR1Anim.SetBool("AR1Up", true);

			yield return new WaitForSecondsRealtime(0.3f);

			AR1Anim.SetBool("AR1Up", false);
			MainAR1.instance.shootable = true;
			WeaponChange.instance.ChangeTrigger = true;
			isPillsUsing = false;

			for (int i = 0; i < 6; i++)
			{
				FirstPersonController.instance.walkSpeed = 6.5f;
				currentHP += 7;
				yield return new WaitForSecondsRealtime(10f);
				FirstPersonController.instance.walkSpeed = 5f;
			}
		}

		//SMG 처리
		else if (SMGOBJ.gameObject.activeSelf == true)
		{
			SMG.instance.shootable = false;
			SMGAnim.SetTrigger("SMGDownTest");

			SMG test = GameObject.Find("SMG_Function").GetComponent<SMG>();
			yield return test.StartCoroutine(test.Rebound());

			yield return new WaitForSecondsRealtime(0.3f);

			SMGOBJ.gameObject.SetActive(false);

			yield return new WaitForSecondsRealtime(3f);

			Pills_Amount -= 1;
			currentBuff = 3600;
			buffBar.value = 60;
			StartCoroutine(BuffTimer());
			FirstPersonController.instance.walkSpeed = 6.5f;

			SMGOBJ.gameObject.SetActive(true);
			SMGAnim.SetTrigger("SMGUpTest");

			yield return new WaitForSecondsRealtime(0.3f);

			SMG.instance.shootable = true;
			WeaponChange.instance.ChangeTrigger = true;
			isPillsUsing = false;

			for (int i = 0; i < 10; i++)
			{
				FirstPersonController.instance.walkSpeed = 6.5f;
				currentHP += 4;
				yield return new WaitForSecondsRealtime(6f);
				FirstPersonController.instance.walkSpeed = 5f;
			}
		}

		//Axe 처리
		else if (Axe.gameObject.activeSelf == true)
		{
			AxeFunction.instance.swingAble = false;
			Axe.gameObject.SetActive(false);

			yield return new WaitForSecondsRealtime(3f);

			Pills_Amount -= 1;
			currentBuff = 3600;
			buffBar.value = 60;
			StartCoroutine(BuffTimer());

			FirstPersonController.instance.walkSpeed = 5f;
			AxeFunction.instance.swingAble = true;
			Axe.gameObject.SetActive(true);
			isPillsUsing = false;
			WeaponChange.instance.ChangeTrigger = true;

			for (int i = 0; i < 6; i++)
			{
				FirstPersonController.instance.walkSpeed = 6.5f;
				currentHP += 7;
				yield return new WaitForSecondsRealtime(10f);
				FirstPersonController.instance.walkSpeed = 5f;
			}
		}
	}

	IEnumerator BuffTimer()
	{
		for (int i = 0; i < 60; i++) 
		{
			yield return new WaitForSecondsRealtime(1f);
			currentBuff -= 60;
			buffBar.value -= 1;
		}
	}
}
