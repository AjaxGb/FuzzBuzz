using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomExit : MonoBehaviour {
	public static RoomExit Inst { get; private set; }

	[SceneName]
	public string nextScene;

	public HashSet<Collider2D> allTouching = new HashSet<Collider2D>();

	void Awake() {
		if (Inst) {
			Debug.LogWarning("Two RoomExits active!");
		}
		Inst = this;
	}

	void OnTriggerEnter2D(Collider2D other) {
		allTouching.Add(other);
		PossessableBase body = other.GetComponentInParent<PossessableBase>();
		if (body) {
			CheckBody(body);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		allTouching.Remove(other);
	}

	public void CheckBody(PossessableBase body) {
		if (body.Possessor && body.Possessor.realBody == body
				&& allTouching.Any((c) => c.GetComponentInParent<PossessableBase>() == body)) {
			SceneManager.LoadScene(nextScene);
		}
	}

	public static void CheckForExit(PossessableBase body) {
		if (Inst) Inst.CheckBody(body);
	}
}
