using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Physics2DMovement))]
public class RoombaBody : PossessableBase {

	public Animator animator;
	public Physics2DMovement movement;

	private int animKeyPossessed;

	void Start() {
		if (!animator) animator = GetComponent<Animator>();
		if (!movement) movement = GetComponent<Physics2DMovement>();

		animKeyPossessed = Animator.StringToHash("Possessed");
	}

	public override void PossessedUpdate() {
		movement.Move(Input.GetAxis("Horizontal"), Input.GetButton("Jump") ? 1 : 0);
	}

	protected override void OnPossessed() {
		animator.SetBool(animKeyPossessed, true);
	}

	protected override void OnUnpossessed() {
		animator.SetBool(animKeyPossessed, false);
	}
}
