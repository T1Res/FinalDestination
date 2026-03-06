using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackFunction : MonoBehaviour
{
    public float attackDelay = 0.2f;
    private float currentDelay = 0;
	private bool attackAble;

	public int damage = 1;

	private void Awake()
	{
		currentDelay = 0;
		attackAble = true;
	}

	private void Update()
	{
		if(currentDelay <= 0)
		{
			currentDelay = attackDelay;
			attackAble = true;
		}

		if (!attackAble)
		{
			currentDelay -= Time.deltaTime;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Player"))
        {
			if(attackAble)
			{
				PlayerHP.instance.currentHP -= damage;
				attackAble = false;
				TransparentManager.instance.transparency += 0.2f;
			}
        }
	}
}
