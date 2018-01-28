using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToNextRoom : MonoBehaviour {

	public string inputName = "Next Room";
	[SceneName]
	public string nextScene;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown(inputName)) {
			SceneManager.LoadScene(nextScene);
		}
	}
}
