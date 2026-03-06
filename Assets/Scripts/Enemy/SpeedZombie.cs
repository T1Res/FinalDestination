using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpeedZombie : MonoBehaviour
{
    private Animator anim;
    public int zombieHP;
    private bool deathFlag = false;

    public GameObject ArmL;
    public GameObject ArmR;

	public bool isBurning;
	public bool burnFlag;



	AudioSource audioSource;

	public AudioClip IdleSound;
	public AudioClip ChaseSound;
	public AudioClip AttackSound;
	public AudioClip DieSound;

	float idleAudioTime;
	bool idleTrigger;
	bool chaseTrigger;

	void Start()
    {
		audioSource = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
        zombieHP = 100;

		isBurning = false;
		burnFlag = false;

		chaseTrigger = true;
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

            anim.SetTrigger("isDead");

			deathFlag = true;
		}

        if(deathFlag)
        {
            StartCoroutine(DeathClear());
        }

        if (GetComponentInParent<ZombieChase>().chase)
        {
			anim.SetBool("isRunning", true);
		}

		if (GetComponentInParent<ZombieChase>().attackTrigger)
		{
            StartCoroutine(Attack());
		}

		if (isBurning && !burnFlag)
		{
			StartCoroutine(burning());
		}

		if (anim.GetBool("isRunning") && chaseTrigger)
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
        deathFlag = false;
		chaseTrigger = false;
		audioSource.Stop();
		yield return new WaitForSecondsRealtime(0.3f);
		audioSource.clip = DieSound;
		audioSource.Play();
		audioSource.Play();
		yield return new WaitForSecondsRealtime(15);
        Destroy(this.gameObject);
    }

    IEnumerator Attack()
    {
		GetComponentInParent<ZombieChase>().attackTrigger = false;
		GetComponentInParent<ZombieChase>().ParentAttackTrigger = false;
		int a = Random.Range(1, 3);
		anim.SetInteger("randomAttack", a);
        anim.SetBool("Attack", true);
		anim.SetBool("isRunning", false);
		ArmL.gameObject.SetActive(true);
		ArmR.gameObject.SetActive(true);

		yield return new WaitForSecondsRealtime(0.3f);

		anim.SetBool("Attack", false);

		yield return new WaitForSecondsRealtime(0.2f);

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
