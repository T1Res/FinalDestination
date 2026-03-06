using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarControl : MonoBehaviour
{
	Slider HPBar;
	public Image fillImage;

	void Start()
	{
		HPBar = GetComponent<Slider>();
	}

	void Update()
	{
		if (HPBar.value <= 0)
			transform.Find("Fill Area").gameObject.SetActive(false);
		else
			transform.Find("Fill Area").gameObject.SetActive(true);

		if(HPBar.value <= 0.3)
			fillImage.color = Color.red;

		if (HPBar.value > 0.3)
			fillImage.color = Color.white;
	}
}
