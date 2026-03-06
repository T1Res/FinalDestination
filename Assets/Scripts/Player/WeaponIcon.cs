using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIcon : MonoBehaviour
{
    public GameObject AR1_Main;
    public GameObject AR1_Sub;

	public GameObject SMG_Main;
	public GameObject SMG_Sub;

	public GameObject Pistol_Main;
    public GameObject Pistol_Sub;

	public GameObject Axe_Main;
	public GameObject Axe_Sub;

	public GameObject molotov;

	void Update()
    {
        //주무기가 AR1 일때
        if(WeaponChange.instance.primaryWeapon == "AR1")
        {
            //현재 들고 있는 무기가 AR1 이라면
            if(WeaponChange.instance.currentHoldingWeapon == "AR1")
            {
				AR1_Main.gameObject.SetActive(true);
                Pistol_Sub.gameObject.SetActive(true);

                AR1_Sub.gameObject.SetActive(false);
                Pistol_Main.gameObject.SetActive(false);

				SMG_Main.gameObject.SetActive(false);
				SMG_Sub.gameObject.SetActive(false);

				Axe_Main.gameObject.SetActive(false);
				Axe_Sub.gameObject.SetActive(false);
			}

            //현재 들고 있는 무기가 Pistol 이라면
			if (WeaponChange.instance.currentHoldingWeapon == "Pistol")
			{
				AR1_Sub.gameObject.SetActive(true);
				Pistol_Main.gameObject.SetActive(true);

				AR1_Main.gameObject.SetActive(false);
				Pistol_Sub.gameObject.SetActive(false);

				SMG_Main.gameObject.SetActive(false);
				SMG_Sub.gameObject.SetActive(false);

				Axe_Main.gameObject.SetActive(false);
				Axe_Sub.gameObject.SetActive(false);
			}
		}

		//주무기가 SMG 일때
		if (WeaponChange.instance.primaryWeapon == "SMG")
		{
			//현재 들고 있는 무기가 SMG 이라면
			if (WeaponChange.instance.currentHoldingWeapon == "SMG")
			{
				SMG_Main.gameObject.SetActive(true);
				Pistol_Sub.gameObject.SetActive(true);

				SMG_Sub.gameObject.SetActive(false);
				Pistol_Main.gameObject.SetActive(false);

				AR1_Main.gameObject.SetActive(false);
				AR1_Sub.gameObject.SetActive(false);

				Axe_Main.gameObject.SetActive(false);
				Axe_Sub.gameObject.SetActive(false);
			}

			//현재 들고 있는 무기가 Pistol 이라면
			if (WeaponChange.instance.currentHoldingWeapon == "Pistol")
			{
				SMG_Sub.gameObject.SetActive(true);
				Pistol_Main.gameObject.SetActive(true);

				SMG_Main.gameObject.SetActive(false);
				Pistol_Sub.gameObject.SetActive(false);

				AR1_Main.gameObject.SetActive(false);
				AR1_Sub.gameObject.SetActive(false);

				Axe_Main.gameObject.SetActive(false);
				Axe_Sub.gameObject.SetActive(false);
			}
		}

		//주무기가 Axe 일때
		if (WeaponChange.instance.primaryWeapon == "Axe")
		{
			//현재 들고 있는 무기가 Axe 이라면
			if (WeaponChange.instance.currentHoldingWeapon == "Axe")
			{
				Axe_Main.gameObject.SetActive(true);
				Pistol_Sub.gameObject.SetActive(true);

				Axe_Sub.gameObject.SetActive(false);
				Pistol_Main.gameObject.SetActive(false);

				AR1_Main.gameObject.SetActive(false);
				AR1_Sub.gameObject.SetActive(false);

				SMG_Main.gameObject.SetActive(false);
				SMG_Sub.gameObject.SetActive(false);
			}

			//현재 들고 있는 무기가 Pistol 이라면
			if (WeaponChange.instance.currentHoldingWeapon == "Pistol")
			{
				Axe_Sub.gameObject.SetActive(true);
				Pistol_Main.gameObject.SetActive(true);

				Axe_Main.gameObject.SetActive(false);
				Pistol_Sub.gameObject.SetActive(false);

				AR1_Main.gameObject.SetActive(false);
				AR1_Sub.gameObject.SetActive(false);

				SMG_Main.gameObject.SetActive(false);
				SMG_Sub.gameObject.SetActive(false);
			}
		}

		//주무기가 없을 때
		if (WeaponChange.instance.primaryWeapon == null)
        {
			Pistol_Main.gameObject.SetActive(true);

			AR1_Sub.gameObject.SetActive(false);
			AR1_Main.gameObject.SetActive(false);
			Pistol_Sub.gameObject.SetActive(false);
			SMG_Main.gameObject.SetActive(false);
			SMG_Sub.gameObject.SetActive(false);
			Axe_Main.gameObject.SetActive(false);
			Axe_Sub.gameObject.SetActive(false);
		}

		if(AmmoFunction.instance.hasMolotov == 1)
		{
			molotov.gameObject.SetActive(true);
		}
		else if (AmmoFunction.instance.hasMolotov == 0)
		{
			molotov.gameObject.SetActive(false);
		}
	}
}
