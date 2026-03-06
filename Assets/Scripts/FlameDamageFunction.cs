using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDamageFunction : MonoBehaviour
{
	public GameObject flameAttach;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			if(other.gameObject.GetComponentInParent<Zombie>().isBurning == false)
			{
				Instantiate(flameAttach, other.gameObject.transform.position, Quaternion.identity).transform.parent = other.transform;
				other.gameObject.GetComponentInParent<Zombie>().isBurning = true;
			}
		}

		else if (other.gameObject.tag == "Enemy2")
		{
			if(other.gameObject.GetComponentInParent<Zombie2>().isBurning == false)
			{
				Instantiate(flameAttach, other.gameObject.transform.position, Quaternion.identity).transform.parent = other.transform;
				other.gameObject.GetComponentInParent<Zombie2>().isBurning = true;
			}
		}

		else if (other.gameObject.tag == "SlowEnemy")
		{
			if (other.gameObject.GetComponentInParent<SlowZombie>().isBurning == false)
			{
				Instantiate(flameAttach, other.gameObject.transform.position, Quaternion.identity).transform.parent = other.transform;
				other.gameObject.GetComponentInParent<SlowZombie>().isBurning = true;
			}
		}

		else if (other.gameObject.tag == "SpeedEnemy")
		{
			if (other.gameObject.GetComponentInParent<SpeedZombie>().isBurning == false)
			{
				Instantiate(flameAttach, other.gameObject.transform.position, Quaternion.identity).transform.parent = other.transform;
				other.gameObject.GetComponentInParent<SpeedZombie>().isBurning = true;
			}
		}
	}
}
