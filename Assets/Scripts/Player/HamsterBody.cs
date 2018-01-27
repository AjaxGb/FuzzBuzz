using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HamsterBody : PossessableBase {

	public bool isAlive = true;

	public float walkSpeed = 100f;
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
		float targetXSpeed = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime;
		float deltaXSpeed = targetXSpeed - rb.velocity.x;
		rb.velocity += Vector2.right * Mathf.Clamp(deltaXSpeed, -walkAccel, walkAccel);

		// Jump (testing only)
		if (Input.GetButton("Jump")) {
			rb.velocity += Vector2.up * jumpStrength * Time.deltaTime;
		}

		// Possess
		if (Input.GetButton("Possess")) {
			PossessableBase target = PossessableBase.allActive
				.Where(p => p.CanBePossessed(Possessor))
				.MinBy((k) => Vector3.Distance(k.transform.position, transform.position));
			Possessor.Possess(target);
		}
	}

	protected override void OnPossessed() {
		animator.SetBool(animKeyPossessed, true);
	}

	protected override void OnUnpossessed() {
		animator.SetBool(animKeyPossessed, false);
	}
}
