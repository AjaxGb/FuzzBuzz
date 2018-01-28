using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Physics2DMovement))]
public class RoombaBody : PossessableBase {

	public Animator animator;
	public Physics2DMovement movement;

	public float unpossessedSpeedMult = 0.5f;
	public float reverseRaycastLength = 3f;
	public bool movingLeft;

	private int animKeyPossessed;
	private int noRaycastLayer;

	void Start() {
		if (!animator) animator = GetComponent<Animator>();
		if (!movement) movement = GetComponent<Physics2DMovement>();

		animKeyPossessed = Animator.StringToHash("Possessed");
		noRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
	}

	void FixedUpdate() {
		if (IsPossessed) return;

		Vector2 moveDirection = movingLeft ? Vector2.left : Vector2.right;
		int layer = gameObject.layer;
		gameObject.layer = noRaycastLayer;

		Debug.DrawRay(transform.position, moveDirection * reverseRaycastLength, Color.red);
		RaycastHit2D hit;
		if (hit = Physics2D.Raycast(transform.position, moveDirection, reverseRaycastLength)) {
			Debug.Log(hit.collider.gameObject);
			movingLeft = !movingLeft;
		}

		gameObject.layer = layer;

		movement.Move(movingLeft ? -unpossessedSpeedMult : unpossessedSpeedMult, 0);
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
