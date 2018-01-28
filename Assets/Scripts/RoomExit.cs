using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomExit : MonoBehaviour {
	[SceneName]
	public string nextScene;

	void OnTriggerEnter2D(Collider2D other) {
		PossessableBase body = other.GetComponentInParent<PossessableBase>();
		if (body && body.Possessor && body.Possessor.realBody == body) {
			SceneManager.LoadScene(nextScene);
		}
	}
}
