using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaBody : PossessableBase {
	public float walkSpeed = 200f;
	public float walkAccel = 500f;
	public float jumpStrength = 10f;

	public Animator animator;
	public Rigidbody2D rb;

	private int animKeyPossessed;

	void Start() {
		if (!animator) animator = GetComponent<Animator>();
		if (!rb) rb = GetComponent<Rigidbody2D>();

		animKeyPossessed = Animator.StringToHash("Possessed");
	}

	public override void PossessedUpdate() {
		// Walk
		Debug.Log("Roomba");

		float targetXSpeed = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime;
		float deltaXSpeed = targetXSpeed - rb.velocity.x;
		rb.velocity += Vector2.right * Mathf.Clamp(deltaXSpeed, -walkAccel, walkAccel);

		if (Input.GetButton("Jump")) {
			rb.velocity += Vector2.up * jumpStrength * Time.deltaTime;
		}
	}

	protected override void OnPossessed() {
		animator.SetBool(animKeyPossessed, true);
	}

	protected override void OnUnpossessed() {
		animator.SetBool(animKeyPossessed, false);
	}
}
