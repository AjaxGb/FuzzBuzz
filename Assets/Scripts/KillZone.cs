using UnityEngine;

public class KillZone : MonoBehaviour {

	public string killMessage = "ZAPPED!";

	void OnTriggerEnter2D(Collider2D other) {
		PossessableBase body = other.GetComponentInParent<PossessableBase>();
		if (body is HamsterBody) {
			GameOverManager.GameOver(killMessage);
		}
	}
}
