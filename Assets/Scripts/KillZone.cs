using UnityEngine;

public class KillZone : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		PossessableBase body = other.GetComponentInParent<PossessableBase>();
		if (body is HamsterBody) {
			GameOverManager.GameOver("ZAPPED!");
		}
	}
}
