using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameEffectLife : MonoBehaviour
{
    private float lifeTime = 10f;

	private void Update()
	{
		lifeTime -= Time.deltaTime;
		if(lifeTime < 0)
		{
			StartCoroutine(Smallize(new Vector3(0.1f, 0.1f, 0.1f)));
		}
	}

	private IEnumerator Smallize(Vector3 size)
	{
		while (transform.lossyScale.x >= size.x)
		{
			transform.localScale *= 0.95f;
			yield return new WaitForSecondsRealtime(1f);
		}

		Destroy(this.gameObject);
	}
}
