using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBody : PossessableBase {

	public float ringDelay = 1;
	public AudioSource audioSource;

	void Start() {
		if (!audioSource) audioSource = GetComponent<AudioSource>();
	}

	protected override void OnPossessed() {
		Possessor.ReturnToBody();

		audioSource.PlayDelayed(ringDelay);
	}

	public override void PossessedUpdate() {}
	protected override void OnUnpossessed() {}
}
