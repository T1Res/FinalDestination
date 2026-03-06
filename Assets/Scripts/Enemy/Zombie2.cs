using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie2 : MonoBehaviour
{
    private Animator anim;
    public int zombieHP;
    private bool deathFlag = false;

    public GameObject ArmL;
    public GameObject ArmR;

	public bool isBurning;
	public bool burnFlag;

	AudioSource audioSource;
	public AudioClip ChaseSound;
	bool chaseTrigger;

	void Start()
    {
        anim = GetComponent<Animator>();
        zombieHP = 150;

		audioSource = GetComponent<AudioSource>();
		chaseTrigger = true;

		isBurning = false;
		burnFlag = false;
	}

	void Update()
    {
		if (GetComponentInParent<ZombieChase>().chase)
		{
			anim.SetBool("IsRunning", true);
		}

		if (zombieHP <= 0)
        {
            //자식 오브젝트들의 캡슐 콜라이더들을 전부 배열에 넣고 비활성화
			CapsuleCollider[] capsule = this.gameObject.GetComponentsInChildren<CapsuleCollider>();
            for(int i = 0; i < capsule.Length; i++)
            {
                capsule[i].enabled = false;
            }

			//자식 오브젝트들의 박스 콜라이더들을 전부 배열에 넣고 비활성화
			BoxCollider[] box = this.gameObject.GetComponentsInChildren<BoxCollider>();
			for (int i = 0; i < box.Length; i++)
			{
				box[i].enabled = false;
			}
			this.gameObject.GetComponent<BoxCollider>().enabled = true;

			if (this.gameObject.GetComponent<NavMeshAgent>() != null && this.gameObject.GetComponent<Rigidbody>() != null)
			{
				this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
			}
            this.gameObject.GetComponent<ZombieChase>().enabled = false;

            anim.SetBool("death", true);
        }

        if(deathFlag)
        {
            StartCoroutine(DeathClear());
        }

		if (GetComponentInParent<ZombieChase>().attackTrigger)
		{
            StartCoroutine(Attack());
		}

		if (isBurning && !burnFlag)
		{
			StartCoroutine(burning());
		}

		if (anim.GetBool("IsRunning") && chaseTrigger)
		{
			if (!audioSource.isPlaying)
			{
				audioSource.PlayOneShot(ChaseSound);
			}
		}
	}

	//사망하면 특정 시간 후 소멸
	IEnumerator DeathClear()
    {
		audioSource.Stop();
		deathFlag = false;
        yield return new WaitForSecondsRealtime(15);
        Destroy(this.gameObject);
    }

    IEnumerator Attack()
    {
		GetComponentInParent<ZombieChase>().attackTrigger = false;
		GetComponentInParent<ZombieChase>().ParentAttackTrigger = false;
		anim.SetTrigger("attack");
        ArmL.gameObject.SetActive(true);
		ArmR.gameObject.SetActive(true);

		yield return new WaitForSecondsRealtime(0.5f);

		ArmL.gameObject.SetActive(false);
		ArmR.gameObject.SetActive(false);
		GetComponentInParent<ZombieChase>().ParentAttackTrigger = true;
	}

	IEnumerator burning()
	{
		burnFlag = true;
		for (int i = 0; i < 11; i++)
		{
			zombieHP -= 12;
			yield return new WaitForSecondsRealtime(1);
		}
		burnFlag &= false;
		isBurning = false;
	}
}
