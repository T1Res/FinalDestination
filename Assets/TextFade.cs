using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour
{
	TMP_Text text;
	public GameObject nextText;

	void Awake()
	{
		text = GetComponent<TMP_Text>();
		text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
		StartCoroutine(FadeTextToFullAlpha());
	}

	public IEnumerator FadeTextToFullAlpha() // 알파값 0에서 1로 전환
	{
		yield return new WaitForSecondsRealtime(2f);
		text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
		while (text.color.a < 1.0f)
		{
			text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 1.0f));
			yield return null;
		}
		yield return new WaitForSecondsRealtime(2f);
		StartCoroutine(FadeTextToZero());
	}

	public IEnumerator FadeTextToZero()  // 알파값 1에서 0으로 전환
	{
		text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
		while (text.color.a > 0.0f)
		{
			text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 1.0f));
			yield return null;
		}
		nextText.gameObject.SetActive(true);
		this.gameObject.SetActive(false);
	}
}
