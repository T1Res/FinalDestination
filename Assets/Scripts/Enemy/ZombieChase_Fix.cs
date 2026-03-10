using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieChase_Fix : MonoBehaviour
{
	private Transform _target;
	private NavMeshAgent _agent;
	private float _sqrAttackRange = 3.24f; // 1.8 * 1.8
	public bool chase = false;
	public bool attackTrigger = false;

	public bool ParentAttackTrigger;

	private void Awake()
	{
		ParentAttackTrigger = true;
	}

	void Start()
	{
		_agent = GetComponent<NavMeshAgent>();
		// 플레이어를 한 번만 찾기
		GameObject player = GameObject.FindWithTag("Player");
		if (player != null) _target = player.transform;

		// 코루틴 시작
		StartCoroutine(UpdatePath());
	}

	IEnumerator UpdatePath()
	{
		while (true)
		{
			if (_target != null && chase)
			{
				_agent.destination = _target.position;
			}
			yield return new WaitForSeconds(0.2f); // 0.2초 대기
		}
	}

	void Update()
	{
		if (_target == null) return;

		// sqrMagnitude로 최적화
		float sqrDistance = (_target.position - transform.position).sqrMagnitude;

		if (sqrDistance < _sqrAttackRange)
		{
			if (ParentAttackTrigger) attackTrigger = true;
		}
	}
}