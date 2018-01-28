using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeoutToNextRoom : MonoBehaviour {

	public float waitTime = 5;
	[SceneName]
	public string nextScene;

	// Use this for initialization
	void Start () {
		StartCoroutine(Transition());
	}

	IEnumerator Transition() {
		AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(nextScene);
		sceneLoad.allowSceneActivation = false;

		yield return new WaitForSeconds(waitTime);

		sceneLoad.allowSceneActivation = true;
	}
}
