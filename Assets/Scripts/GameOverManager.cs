using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {
	public static GameOverManager Inst { get; private set; }

	public GameObject[] deathDisplayObjects;
	public Text deathMessageText;

	public float waitTime = 3f;

	private bool isGameOver;

	// Use this for initialization
	void Start() {
		Inst = this;
		if (!deathMessageText) deathMessageText = GetComponentInChildren<Text>();

		foreach (GameObject go in deathDisplayObjects) {
			go.SetActive(false);
		}
	}

	public static void GameOver(string deathMessage) {
		if (Inst) {
			Inst.OnGameOver(deathMessage);
		} else {
			Debug.LogError("GameOverManager not found");
		}
	}
	
	private void OnGameOver(string deathMessage) {
		if (isGameOver) return;

		deathMessageText.text = deathMessage;

		foreach (GameObject go in deathDisplayObjects) {
			go.SetActive(true);
		}

		isGameOver = true;

		PlayerMind.Inst.stopUpdates = true;
		Time.timeScale = 0;

		StartCoroutine(RestartLevel(waitTime));
	}

	private IEnumerator RestartLevel(float wait) {
		AsyncOperation loadScene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
		loadScene.allowSceneActivation = false;
		yield return new WaitForSecondsRealtime(wait);
		loadScene.allowSceneActivation = true;
		loadScene.completed += ResumeTimeflow;
		if (loadScene.isDone) ResumeTimeflow(null);
	}

	private void ResumeTimeflow(AsyncOperation obj) {
		Time.timeScale = 1;
	}
}
