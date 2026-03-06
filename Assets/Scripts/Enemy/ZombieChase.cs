using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieChase : MonoBehaviour
{
	public Transform target;
	NavMeshAgent agent;
	public bool chase = false;
	public float distance;
	public bool attackTrigger = false;
	public bool ParentAttackTrigger;

	private void Awake()
	{
		ParentAttackTrigger = true;
	}

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		//agent.speed *= Time.deltaTime;

		target = GameObject.FindWithTag("Player").transform;

		if (chase)
		{
			agent.destination = target.transform.position;
		}

		distance = Vector3.Distance(target.transform.position, this.gameObject.transform.position);

		if (distance < 1.8)
		{
			if(ParentAttackTrigger) 
			{
				attackTrigger = true;
			}
		}
	}
}