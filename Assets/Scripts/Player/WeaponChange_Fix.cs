using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.Burst.Intrinsics;

// enum쓰기
public enum WeaponType
{
	None,
	Pistol,
	AR1,
	SMG,
	Axe
}

public class WeaponChange_Fix : MonoBehaviour
{
	public static WeaponChange_Fix instance;

	[Header("Weapon Objects")]
	public GameObject PistolOBJ;
	public GameObject AR1OBJ;
	public GameObject AxeOBJ;
	public GameObject SMGOBJ;

	[Header("Weapon Recoil")]
	private AR1RecoilFunction cachedAR1Recoil;

	[Header("UI & Trigger")]
	public TextMeshProUGUI warnText;
	public bool ChangeTrigger = true;

	[Header("Weapon States")]
	// 2. String 변수를 Enum 타입으로 교체
	public WeaponType primaryWeapon = WeaponType.AR1;
	public WeaponType currentHoldingWeapon = WeaponType.Pistol;

	

	private void Awake()
	{
		instance = this;
		// 여기에 오브젝트들 캐싱해두기
		cachedAR1Recoil = GetComponentInChildren<AR1RecoilFunction>();
	}

	void Update()
	{
		if (!ChangeTrigger) return;

		if (Input.GetKeyDown(KeyCode.Alpha1) && currentHoldingWeapon != primaryWeapon)
		{
			TrySwitchWeapon(primaryWeapon);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2) && currentHoldingWeapon != WeaponType.Pistol)
		{
			TrySwitchWeapon(WeaponType.Pistol); // 권총을 보조무기로 고정
		}
	}

	// 무기 교체 가능 여부 검사
	private void TrySwitchWeapon(WeaponType targetWeapon)
	{
		bool cannotSwitch = false;

		if (AmmoFunction.instance != null && AmmoFunction.instance.isReloading) cannotSwitch = true;
		if (AxeFunction.instance != null && AxeFunction.instance.isSwinging) cannotSwitch = true;

		if (cannotSwitch)
		{
			StartCoroutine(Warning());
		}
		else
		{
			StartCoroutine(PerformWeaponSwitch(targetWeapon));
		}
	}

	// 모든 무기 교체 담당 단일 코루틴
	private IEnumerator PerformWeaponSwitch(WeaponType targetWeapon)
	{
		ChangeTrigger = false;

		// 현재 무기 비활성화
		switch (currentHoldingWeapon)
		{
			case WeaponType.Pistol:
				PistolOBJ.SetActive(false);
				break;
			case WeaponType.AR1:
				AR1OBJ.SetActive(false);
				break;
				// SMG, Axe 추가해야함
		}

		// 애니메이션/교체 대기 시간 
		yield return new WaitForSecondsRealtime(0.5f);

		// 대상 무기 활성화
		switch (targetWeapon)
		{
			case WeaponType.Pistol:
				PistolOBJ.SetActive(true);
				// GetComponent 쓰지 않기 !!!
				break;
			case WeaponType.AR1:
				AR1OBJ.SetActive(true);
				break;
				// SMG, Axe 도 추가
		}

		currentHoldingWeapon = targetWeapon;
		ChangeTrigger = true;
	}

	IEnumerator Warning()
	{
		warnText.gameObject.SetActive(true);
		yield return new WaitForSecondsRealtime(1.0f);
		warnText.gameObject.SetActive(false);
	}
}