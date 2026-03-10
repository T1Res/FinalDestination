using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieChase2 : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			GetComponentInParent<ZombieChase>().chase = true;
			GetComponentInParent<ZombieChase_Fix>().chase = true;
		}
	}
}
