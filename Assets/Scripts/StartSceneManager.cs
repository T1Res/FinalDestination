using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public void StartGameButton()
    {
		SceneLoader.Instance.ChangeScene();
	}

	public void HelpButton()
	{
		SceneManager.LoadScene("Help");
	}

	public void ExitButton()
    {
        Application.Quit();
    }
}
