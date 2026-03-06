using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SceneLoader : MonoBehaviour
{
	public CanvasGroup Fade_img;
	float fadeDuration = 2;

	public GameObject Loading;
	public GameObject button;
	public TMP_Text Loading_text;


	public static SceneLoader Instance
	{
		get
		{
			return instance;
		}
	}
	private static SceneLoader instance;

	void Start()
	{
		if (instance != null)
		{
			DestroyImmediate(this.gameObject);
			return;
		}
		instance = this;

		DontDestroyOnLoad(gameObject);

		SceneManager.sceneLoaded += OnSceneLoaded; // 이벤트에 추가
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트에서 제거*
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Fade_img.DOFade(0, fadeDuration)
		.OnStart(() => {
			Loading.SetActive(false);
		})
		.OnComplete(() => {
			Fade_img.blocksRaycasts = false;
		});
	}

	public void ChangeScene()
	{
		Fade_img.DOFade(1, fadeDuration)
		.OnStart(() => {
			Fade_img.blocksRaycasts = true; //아래 레이캐스트 막기
		})
		.OnComplete(() => {
			StartCoroutine(LoadScene("GameScene"));
		});
	}

	IEnumerator LoadScene(string sceneName)
	{
		Loading.SetActive(true); //로딩 화면을 띄움

		AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
		async.allowSceneActivation = false; //퍼센트 딜레이용

		float past_time = 0;
		float percentage = 0;

		while (!(async.isDone))
		{
			yield return null;

			past_time += Time.deltaTime;

			if (percentage >= 90)
			{
				percentage = Mathf.Lerp(percentage, 100, past_time);

				if (percentage == 100)
				{
					async.allowSceneActivation = true; //씬 전환 준비 완료
				}
			}
			else
			{
				percentage = Mathf.Lerp(percentage, async.progress * 100f, past_time);
				if (percentage >= 90) past_time = 0;
			}
			Loading_text.text = percentage.ToString("0"); //로딩 퍼센트 표기
		}
	}
}
