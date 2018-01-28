using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyGaze : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		PossessableBase body = other.GetComponentInParent<PossessableBase>();
		if (body.Possessor || body is HamsterBody) {
			GameOverManager.GameOver("SPOTTED!");
		}
	}
}
