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
	private int animKeyInputStrength;
	private int animKeyJump;

	private float liveGravity;
	private float emptyGravity;

	void Start() {
		if (!animator) animator = GetComponent<Animator>();
		if (!movement) movement = GetComponent<Physics2DMovement>();

		liveGravity = movement.rb.gravityScale;
		emptyGravity = liveGravity * emptyGravityScale;

		animKeyPossessed = Animator.StringToHash("Possessed");
		animKeyInputStrength = Animator.StringToHash("InputStrength");
		animKeyJump = Animator.StringToHash("Jump");
	}

	public override void PossessedUpdate() {
		float horizInput = Input.GetAxis("Horizontal");
		float vertInput = 0;

		bool onGround = true;
		if (Input.GetButtonDown("Jump") && onGround) {
			vertInput = 1;
			animator.SetTrigger(animKeyJump);
		}

		Vector3 currScale = transform.localScale;
		if (horizInput < -0.1f) {
			currScale.x = -1;
		} else if (horizInput > 0.1f) {
			currScale.x = 1;
		}
		transform.localScale = currScale;

		animator.SetFloat(animKeyInputStrength, Mathf.Abs(horizInput));
		movement.Move(horizInput, vertInput);
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
