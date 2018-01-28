using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Physics2DMovement))]
public class HamsterBody : PossessableBase {

	public bool isAlive = true;

	public float emptyGravityScale = 2f;
	
	public Animator animator;
	public Physics2DMovement movement;

	private int animKeyPossessed;

	private float liveGravity;
	private float emptyGravity;

	void Start() {
		if (!animator) animator = GetComponent<Animator>();
		if (!movement) movement = GetComponent<Physics2DMovement>();

		liveGravity = movement.rb.gravityScale;
		emptyGravity = liveGravity * emptyGravityScale;

		animKeyPossessed = Animator.StringToHash("Possessed");
	}

	public override void PossessedUpdate() {
		float horizInput = Input.GetAxis("Horizontal");

		Vector3 currScale = transform.localScale;
		if (horizInput < -0.1f) {
			currScale.x = -1;
		} else if (horizInput > 0.1f) {
			currScale.x = 1;
		}
		transform.localScale = currScale;

		movement.Move(horizInput, Input.GetButton("Jump") ? 1 : 0);
	}

	protected override void OnPossessed() {
		animator.SetBool(animKeyPossessed, true);
		movement.rb.gravityScale = liveGravity;
	}

	protected override void OnUnpossessed() {
		animator.SetBool(animKeyPossessed, false);
		movement.rb.gravityScale = emptyGravity;
	}
}
