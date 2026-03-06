using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : MonoBehaviour
{
	public GameObject Flame;

	private void Awake()
	{
		Vector3 test = new Vector3 (transform.forward.x, transform.forward.y + 0.35f, transform.forward.z);
		Vector3 test2 = new Vector3(this.gameObject.transform.right.x, this.gameObject.transform.right.y, this.gameObject.transform.right.z);

		Rigidbody rigidMolotov = this.GetComponent<Rigidbody>();
		rigidMolotov.AddForce(test * 15, ForceMode.Impulse);
		//rigidMolotov.AddTorque(Vector3.back * 10, ForceMode.Impulse);
		rigidMolotov.AddTorque(test2 * 0.2f, ForceMode.Impulse);
	}

	private void OnCollisionEnter(Collision collision)
	{
		Instantiate(Flame, transform.position, Quaternion.identity);
		Instantiate(Flame, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
		Instantiate(Flame, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.identity);
		Instantiate(Flame, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Quaternion.identity);
		Instantiate(Flame, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
		Destroy(this.gameObject);
	}
}
