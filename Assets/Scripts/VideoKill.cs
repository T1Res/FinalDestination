using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoKill : MonoBehaviour
{
	public GameObject logo;
	public GameObject button;

	private void Awake()
	{
		StartCoroutine(Kill());
	}

	IEnumerator Kill()
	{
		yield return new WaitForSecondsRealtime(2f);
		logo.SetActive(true);
		button.SetActive(true);

		yield return new WaitForSecondsRealtime(8f);
		Destroy(this.gameObject);
	}
}
