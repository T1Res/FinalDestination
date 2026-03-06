using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgress : MonoBehaviour
{
	public TextMeshProUGUI ProgressIndicator;
	public Image LoadingBar1;
	public Image LoadingBar2;
	public Image LoadingBar3;
	float currentValue;
	float timeleft;
	public float speed;

	private void Awake()
	{
		currentValue = 100;
		timeleft = 6.0f;
	}

	void Update()
	{
		currentValue -= speed * Time.deltaTime;
		timeleft -= Time.deltaTime;
		ProgressIndicator.text = (timeleft.ToString("F1"));

		LoadingBar1.fillAmount = currentValue / 100;
		LoadingBar2.fillAmount = currentValue / 100;
		LoadingBar3.fillAmount = currentValue / 100;

		if (currentValue < 0)
		{
			this.gameObject.SetActive(false);
		}
	}
}
