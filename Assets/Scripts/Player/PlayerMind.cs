using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMind : MonoBehaviour {
	public static PlayerMind inst;

	public PossessableBase currentBody;

	public bool stopUpdates;

	// Use this for initialization
	void Start() {
		if (inst) Debug.LogWarning("Two PlayerMinds in play!");
		inst = this;

		Possess(currentBody);
	}
	
	// Update is called once per frame
	void Update() {
		if (!stopUpdates) {
			currentBody.PossessedUpdate();
		}
	}

	public void Possess(PossessableBase body) {
		if (currentBody) Unpossess();
		currentBody = body;
		currentBody.DoPossess(this);
	}

	public void Unpossess() {
		if (currentBody) currentBody.DoUnpossess();
		currentBody = null;
	}
}
