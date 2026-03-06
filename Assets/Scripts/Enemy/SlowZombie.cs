using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class SlowZombie : MonoBehaviour
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
        zombieHP = 200;

		audioSource = GetComponent<AudioSource>();
		chaseTrigger = true;

		isBurning = false;
		burnFlag = false;
	}

    void Update()
    {
        if(zombieHP <= 0)
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

            //무작위 사망모션
			int a = Random.Range(1, 3);
			anim.SetInteger("isDead", a);
            deathFlag = true;
        }

        if(deathFlag)
        {
            StartCoroutine(DeathClear());
        }

        if (GetComponentInParent<ZombieChase>().chase)
        {
			anim.SetBool("isMoving", true);
		}

		if (GetComponentInParent<ZombieChase>().attackTrigger)
		{
            StartCoroutine(Attack());
		}

		if (isBurning && !burnFlag)
		{
			StartCoroutine(burning());
		}

		if (anim.GetBool("isMoving") && chaseTrigger)
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
		anim.SetBool("Attacking", true);
		anim.SetBool("isMoving", false);
        ArmL.gameObject.SetActive(true);
		ArmR.gameObject.SetActive(true);

		yield return new WaitForSecondsRealtime(0.5f);

		anim.SetBool("Attacking", false);
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
