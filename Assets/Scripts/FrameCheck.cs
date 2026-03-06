using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrameCheck : MonoBehaviour
{
	float deltaTime = 0.0f;
	float fps; 

	public TMP_Text fpsText;

	void Awake()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}

	void Update() 
	{ 
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}

	void OnGUI()
	{
		if (!PauseMenuFunction.GameIsPaused)
		{
			fps = 1.0f / deltaTime;  //초당 프레임 - 1초에 

			fpsText.text = fps.ToString("F0") + " fps";
		}
	}
}
