using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearFunc : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Cursor.lockState = CursorLockMode.None;
			SceneManager.LoadScene("GameClearScene");
		}
	}
}
