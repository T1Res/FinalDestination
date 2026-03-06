
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuFunction : MonoBehaviour
{
	public static bool GameIsPaused = false;
	public GameObject pauseMenuCanvas;
	public GameObject aim1;
	public GameObject aim2;
	public GameObject aim3;
	public GameObject aim4;

	public GameObject FPS;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	public void Resume()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		FirstPersonController.instance.cameraCanMove = true;
		FPS.SetActive(true);
		aim1.SetActive(true); aim2.SetActive(true); aim3.SetActive(true); aim4.SetActive(true);

		pauseMenuCanvas.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	public void Pause()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		FirstPersonController.instance.cameraCanMove = false;
		FPS.SetActive(false);
		aim1.SetActive(false); aim2.SetActive(false); aim3.SetActive(false); aim4.SetActive(false);

		pauseMenuCanvas.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void titleButon()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("TitleScene");
	}

	public void exitButon()
	{
		Application.Quit();
	}
}
