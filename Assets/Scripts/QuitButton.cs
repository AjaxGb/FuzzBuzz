using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour {

	public string quitInput = "Quit";

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown(quitInput)) {
			Application.Quit();
		}
	}
}
