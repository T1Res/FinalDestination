using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class AxeFunction : MonoBehaviour
{
	public static AxeFunction instance;
    private Animator anim;
	private bool dmgTrigger;
	private GameObject HitObj;

	public bool swingAble;
	public bool isSwinging;

	public GameObject BloodEffect;

	private void Awake()
	{
		instance = this;
		anim = GetComponent<Animator>();
		dmgTrigger = true;

		swingAble = true;
	}

	private void Update()
	{
		if(Input.GetMouseButtonDown(0) && swingAble) 
		{ 
			StartCoroutine(AxeSwing());
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			HitObj = other.gameObject;

			if (dmgTrigger)
			{
				StartCoroutine(DamageFuncZombie());

				GameObject blood1 = Instantiate(BloodEffect, new Vector3(HitObj.transform.position.x, HitObj.transform.position.y, HitObj.transform.position.z),
					Quaternion.Euler(Vector3.up));
				Destroy(blood1, 0.3f);
			}
		}

		if (other.gameObject.tag == "Enemy2")
		{
			HitObj = other.gameObject;

			if (dmgTrigger)
			{
				StartCoroutine(DamageFuncZombie2());

				GameObject blood1 = Instantiate(BloodEffect, new Vector3(HitObj.transform.position.x, HitObj.transform.position.y, HitObj.transform.position.z),
					Quaternion.Euler(Vector3.up));
				Destroy(blood1, 0.3f);
			}
		}

		if (other.gameObject.tag == "SlowEnemy")
		{
			HitObj = other.gameObject;

			if (dmgTrigger)
			{
				StartCoroutine(DamageFuncSlowZombie());

				GameObject blood1 = Instantiate(BloodEffect, new Vector3(HitObj.transform.position.x, HitObj.transform.position.y, HitObj.transform.position.z),
					Quaternion.Euler(Vector3.up));
				Destroy(blood1, 0.3f);
			}
		}

		if (other.gameObject.tag == "SpeedEnemy")
		{
			HitObj = other.gameObject;

			if (dmgTrigger)
			{
				StartCoroutine(DamageFuncSpeedZombie());

				GameObject blood1 = Instantiate(BloodEffect, new Vector3(HitObj.transform.position.x, HitObj.transform.position.y, HitObj.transform.position.z),
					Quaternion.Euler(Vector3.up));
				Destroy(blood1, 0.3f);
			}
		}
	}

	IEnumerator AxeSwing()
	{
		isSwinging = true;
		swingAble = false;
		anim.SetBool("AxeAttack", true);
		this.gameObject.GetComponent<BoxCollider>().enabled = true;

		yield return new WaitForSecondsRealtime(0.4f);

		anim.SetBool("AxeAttack", false);
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
		isSwinging = false;
		swingAble = true;
	}

	IEnumerator DamageFuncZombie()
	{
		dmgTrigger = false;
		HitObj.gameObject.GetComponentInParent<Zombie>().zombieHP -= 50;

		yield return new WaitForSecondsRealtime(0.4f);

		dmgTrigger = true;
	}

	IEnumerator DamageFuncZombie2()
	{
		dmgTrigger = false;
		HitObj.gameObject.GetComponentInParent<Zombie2>().zombieHP -= 50;

		yield return new WaitForSecondsRealtime(0.4f);

		dmgTrigger = true;
	}

	IEnumerator DamageFuncSlowZombie()
	{
		dmgTrigger = false;
		HitObj.gameObject.GetComponentInParent<SlowZombie>().zombieHP -= 50;

		yield return new WaitForSecondsRealtime(0.4f);

		dmgTrigger = true;
	}

	IEnumerator DamageFuncSpeedZombie()
	{
		dmgTrigger = false;
		HitObj.gameObject.GetComponentInParent<SpeedZombie>().zombieHP -= 50;

		yield return new WaitForSecondsRealtime(0.4f);

		dmgTrigger = true;
	}
}
